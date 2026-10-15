// ViewComponents/CatalogMenuViewComponent.cs
using Journal.Data;
using Journal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Journal.ViewComponents
{
    public class CatalogMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public CatalogMenuViewComponent(ApplicationDbContext context) => _context = context;

        public IViewComponentResult Invoke(CatalogSection section)
        {
            var types = _context.ProductTypes
                .Where(t => t.Section == section)
                .OrderBy(t => t.SortOrder)
                .ToList();
            return View(types);
        }
    }
}