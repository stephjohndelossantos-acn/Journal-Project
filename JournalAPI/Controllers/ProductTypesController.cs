// JournalAPI/Controllers/ProductTypesController.cs
using Journal.Models;
using JournalAPI.Dtos;
using JournalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JournalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypeService _types;
        public ProductTypesController(IProductTypeService types) => _types = types;

        [HttpGet]   // /api/producttypes?section=1
        public async Task<ActionResult<List<ProductTypeDto>>> Get(CatalogSection? section) =>
            await _types.GetBySectionAsync(section);

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeDto>> GetOne(int id)
        {
            var t = await _types.GetByIdAsync(id);
            return t is null ? NotFound() : t;
        }
    }
}