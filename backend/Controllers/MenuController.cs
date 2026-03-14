using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.DTOs;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menu;
        public MenuController(IMenuService menu) { _menu = menu; }

        [HttpGet][AllowAnonymous]
        public async Task<IActionResult> GetAll() => Ok(await _menu.GetAllAsync());

        [HttpGet("{id}")][AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _menu.GetByIdAsync(id);
            return item == null ? NotFound(new { message = $"Item {id} not found." }) : Ok(item);
        }

        [HttpPost][Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateMenuItemDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = await _menu.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")][Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuItemDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = await _menu.UpdateAsync(id, dto);
            return item == null ? NotFound(new { message = $"Item {id} not found." }) : Ok(item);
        }

        [HttpDelete("{id}")][Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _menu.DeleteAsync(id);
            return deleted ? NoContent() : NotFound(new { message = $"Item {id} not found." });
        }
    }
}
