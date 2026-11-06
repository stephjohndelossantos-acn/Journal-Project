using Journal.Data;
using Journal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Journal.Controllers
{
    public class JournalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly JournalApiClient _api;

        public JournalController(ApplicationDbContext context, JournalApiClient api)
        {
            _context = context;
            _api = api;
        }

        public async Task<IActionResult> Homepage(int type)
        {
            var t = await _api.GetTypeAsync(type);
            if (t == null) return NotFound();

            var result = await _api.GetItemsAsync(type: type);
            ViewBag.TypeName = t.Name;
            return View(result.Items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _api.GetItemAsync(id);
            return item == null ? NotFound() : View(item);
        }

    }

}