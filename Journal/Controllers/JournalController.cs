using Journal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Journal.Controllers
{
    public class JournalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JournalController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Homepage(int type)
        {
            var productType = _context.ProductTypes.Find(type);
            if (productType == null) return NotFound();

            var items = _context.JournalItems
                .Where(i => i.ProductTypeId == type)
                .OrderBy(i => i.SortOrder)
                .ThenBy(i => i.Code)
                .ToList();

            ViewBag.TypeName = productType.Name;
            return View(items);
        }

        public IActionResult Details(int id)
        {
            var item = _context.JournalItems
                .Include(i => i.ProductType)
                .FirstOrDefault(i => i.Id == id);

            if (item == null) return NotFound();
            return View(item);
        }
    }
}