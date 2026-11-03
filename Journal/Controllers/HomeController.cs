using Journal.ApiModels;
using Journal.Data;
using Journal.Models;
using Journal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Journal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JournalApiClient _api;

        public HomeController(ILogger<HomeController> logger, JournalApiClient api)
        {
            _logger = logger;
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var featured = await _api.GetItemsAsync(featured: true, pageSize: 1);
            var sections = new List<TypeSection>();

            foreach (var t in await _api.GetTypesAsync(1))   // 1 = Journal section
            {
                var items = await _api.GetItemsAsync(type: t.Id);
                if (items.Items.Count > 0)
                    sections.Add(new TypeSection { Type = t, Items = items.Items });
            }

            return View(new HomePageModel { Featured = featured.Items.FirstOrDefault(), Sections = sections });
        }
    }
}
