// JournalAPI/Services/ProductTypeService.cs
using Journal.Data;
using Journal.Models;
using JournalAPI.Dtos;
using Microsoft.EntityFrameworkCore;

namespace JournalAPI.Services
{
    public interface IProductTypeService
    {
        Task<List<ProductTypeDto>> GetBySectionAsync(CatalogSection? section);
        Task<ProductTypeDto?> GetByIdAsync(int id);
    }

    public class ProductTypeService : IProductTypeService
    {
        private readonly ApplicationDbContext _context;
        public ProductTypeService(ApplicationDbContext context) => _context = context;

        public Task<List<ProductTypeDto>> GetBySectionAsync(CatalogSection? section)
        {
            var query = _context.ProductTypes.AsNoTracking().AsQueryable();
            if (section.HasValue) query = query.Where(t => t.Section == section.Value);

            return query.OrderBy(t => t.SortOrder)
                .Select(t => new ProductTypeDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Subtitle = t.Subtitle,
                    ImageUrl = t.ImageUrl,
                    Section = (int)t.Section,
                    SortOrder = t.SortOrder
                }).ToListAsync();
        }

        public Task<ProductTypeDto?> GetByIdAsync(int id) =>
            _context.ProductTypes.AsNoTracking().Where(t => t.Id == id)
                .Select(t => new ProductTypeDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Subtitle = t.Subtitle,
                    ImageUrl = t.ImageUrl,
                    Section = (int)t.Section,
                    SortOrder = t.SortOrder
                }).FirstOrDefaultAsync();
    }
}