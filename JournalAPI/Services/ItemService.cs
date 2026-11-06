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
            TypeName = i.ProductType!.Name,
            CreatedAt = i.CreatedAt,
            TypeSubtitle = i.ProductType!.Subtitle
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

        public async Task<PagedResult<ItemDto>> GetPagedAsync(ItemQuery q)
        {
            var query = _context.JournalItems.AsNoTracking().AsQueryable();

            if (q.Type.HasValue)
                query = query.Where(i => i.ProductTypeId == q.Type.Value);
            if (!string.IsNullOrWhiteSpace(q.Search))
                query = query.Where(i => i.Title.Contains(q.Search));
            if (q.Featured.HasValue)
                query = query.Where(i => i.IsFeatured == q.Featured.Value);

            query = q.Sort switch
            {
                "price_asc" => query.OrderBy(i => i.Price),
                "price_desc" => query.OrderByDescending(i => i.Price),
                "newest" => query.OrderByDescending(i => i.CreatedAt),
                _ => query.OrderBy(i => i.SortOrder).ThenBy(i => i.Code)
            };

            var page = Math.Max(q.Page, 1);
            var size = Math.Clamp(q.PageSize, 1, 50);
            var total = await query.CountAsync();

            var items = await query.Skip((page - 1) * size).Take(size)
                                   .Select(ToDto).ToListAsync();

            return new PagedResult<ItemDto> { Items = items, TotalCount = total, Page = page, PageSize = size };
        }
    }
}