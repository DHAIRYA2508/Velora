using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(int id);
        Task<OrderDto> CreateAsync(CreateOrderDto dto);
        Task<OrderDto?> UpdateStatusAsync(int id, string status);
    }

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<OrderDto>> GetAllAsync() =>
            await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
                .OrderByDescending(o => o.OrderDate).Select(o => Map(o)).ToListAsync();

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var o = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem).FirstOrDefaultAsync(o => o.Id == id);
            return o == null ? null : Map(o);
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                CustomerName = dto.CustomerName,
                TotalAmount = dto.OrderItems.Sum(i => i.Price * i.Quantity),
                Status = "Pending"
            };
            foreach (var i in dto.OrderItems)
                order.OrderItems.Add(new OrderItem { MenuItemId = i.MenuItemId, Quantity = i.Quantity, Price = i.Price });

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await _context.Entry(order).Collection(o => o.OrderItems).LoadAsync();
            foreach (var oi in order.OrderItems)
                await _context.Entry(oi).Reference(x => x.MenuItem).LoadAsync();
            return Map(order);
        }

        public async Task<OrderDto?> UpdateStatusAsync(int id, string status)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return null;
            order.Status = status;
            await _context.SaveChangesAsync();
            return Map(order);
        }

        private static OrderDto Map(Order o) => new()
        {
            Id = o.Id, CustomerName = o.CustomerName, TotalAmount = o.TotalAmount,
            OrderDate = o.OrderDate, Status = o.Status,
            OrderItems = o.OrderItems.Select(oi => new OrderItemDto { MenuItemId = oi.MenuItemId, MenuItemName = oi.MenuItem?.Name, Quantity = oi.Quantity, Price = oi.Price }).ToList()
        };
    }
}
