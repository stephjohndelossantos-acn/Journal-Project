using JournalAPI.Dtos;
using JournalAPI.Security;
using JournalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JournalAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _items;
        public ItemsController(IItemService items) => _items = items;

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<ItemDto>>> GetItems([FromQuery] ItemQuery q) =>
            await _items.GetPagedAsync(q);

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            var item = await _items.GetByIdAsync(id);
            return item is null ? NotFound() : item;
        }

        [HttpPost]
        [ApiKey]
        [ProducesResponseType(typeof(ItemDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ItemDto>> Create(CreateItemDto dto)
        {
            var created = await _items.CreateAsync(dto);
            return created is null
                ? BadRequest("ProductTypeId does not exist.")
                : CreatedAtAction(nameof(GetItem), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ApiKey]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, CreateItemDto dto) =>
            await _items.UpdateAsync(id, dto) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        [ApiKey]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) =>
            await _items.DeleteAsync(id) ? NoContent() : NotFound();
    }
}