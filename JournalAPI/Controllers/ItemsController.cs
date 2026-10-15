using JournalAPI.Services;
using JournalAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JournalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _items;
        public ItemsController(IItemService items) => _items = items;

        [HttpGet]
        public async Task<ActionResult<List<ItemDto>>> GetItems(int? type) =>
            await _items.GetByTypeAsync(type);

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            var item = await _items.GetByIdAsync(id);
            return item is null ? NotFound() : item;
        }

        [HttpPost]   // add [Authorize] later
        public async Task<ActionResult<ItemDto>> Create(CreateItemDto dto)
        {
            var created = await _items.CreateAsync(dto);
            return created is null
                ? BadRequest("ProductTypeId does not exist.")
                : CreatedAtAction(nameof(GetItem), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]   // add [Authorize] later
        public async Task<IActionResult> Update(int id, CreateItemDto dto) =>
            await _items.UpdateAsync(id, dto) ? NoContent() : NotFound();

        [HttpDelete("{id}")]   // add [Authorize] later
        public async Task<IActionResult> Delete(int id) =>
            await _items.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
