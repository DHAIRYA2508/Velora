using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.DTOs;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _order;
        public OrderController(IOrderService order) { _order = order; }

        [HttpGet][Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => Ok(await _order.GetAllAsync());

        [HttpGet("{id}")][Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var o = await _order.GetByIdAsync(id);
            return o == null ? NotFound(new { message = $"Order {id} not found." }) : Ok(o);
        }

        [HttpPost][Authorize]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!dto.OrderItems.Any()) return BadRequest(new { message = "Order must have at least one item." });
            var o = await _order.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = o.Id }, o);
        }

        [HttpPut("{id}/status")][Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var valid = new[] { "Pending", "Preparing", "Ready", "Delivered", "Cancelled" };
            if (!valid.Contains(dto.Status)) return BadRequest(new { message = "Invalid status." });
            var o = await _order.UpdateStatusAsync(id, dto.Status);
            return o == null ? NotFound(new { message = $"Order {id} not found." }) : Ok(o);
        }
    }
}
