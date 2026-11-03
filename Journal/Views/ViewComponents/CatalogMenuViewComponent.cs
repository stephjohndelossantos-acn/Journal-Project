// ViewComponents/CatalogMenuViewComponent.cs
using Journal.ApiModels;
using Journal.Data;
using Journal.Models;
using Journal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Journal.ViewComponents
{
    public class CatalogMenuViewComponent : ViewComponent
    {
        private readonly JournalApiClient _api;
        public CatalogMenuViewComponent(JournalApiClient api) => _api = api;

        public async Task<IViewComponentResult> InvokeAsync(CatalogSection section)
        {
            try { return View(await _api.GetTypesAsync((int)section)); }
            catch (HttpRequestException) { return View(new List<ProductTypeDto>()); }
        }
    }
}