using Journal.Data;
using Journal.Models;
using JournalAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JournalAPI.Services
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _context;
        public ItemService(ApplicationDbContext context) => _context = context;

        // Written once, reused by every query
        private static readonly Expression<Func<JournalItem, ItemDto>> ToDto = i => new ItemDto
        {
            Id = i.Id,
            Code = i.Code,
            Title = i.Title,
            Description = i.Description,
            Price = i.Price,
            ImageUrl = i.ImageUrl,
            IsFeatured = i.IsFeatured,
            TypeId = i.ProductTypeId,
            TypeName = i.ProductType!.Name
        };

        public Task<List<ItemDto>> GetByTypeAsync(int? typeId)
        {
            var query = _context.JournalItems.AsNoTracking().AsQueryable();
            if (typeId.HasValue)
                query = query.Where(i => i.ProductTypeId == typeId.Value);

            return query.OrderBy(i => i.SortOrder).ThenBy(i => i.Code)
                        .Select(ToDto).ToListAsync();
        }

        public Task<ItemDto?> GetByIdAsync(int id) =>
            _context.JournalItems.AsNoTracking()
                .Where(i => i.Id == id).Select(ToDto).FirstOrDefaultAsync();

        public async Task<ItemDto?> CreateAsync(CreateItemDto dto)
        {
            if (!await _context.ProductTypes.AnyAsync(t => t.Id == dto.ProductTypeId))
                return null;

            var item = new JournalItem
            {
                Code = dto.Code,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                IsFeatured = dto.IsFeatured,
                SortOrder = dto.SortOrder,
                ProductTypeId = dto.ProductTypeId
            };
            _context.JournalItems.Add(item);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(item.Id);
        }

        public async Task<bool> UpdateAsync(int id, CreateItemDto dto)
        {
            var item = await _context.JournalItems.FindAsync(id);
            if (item is null) return false;

            item.Code = dto.Code; item.Title = dto.Title; item.Description = dto.Description;
            item.Price = dto.Price; item.ImageUrl = dto.ImageUrl; item.IsFeatured = dto.IsFeatured;
            item.SortOrder = dto.SortOrder; item.ProductTypeId = dto.ProductTypeId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.JournalItems.FindAsync(id);
            if (item is null) return false;

            _context.JournalItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}