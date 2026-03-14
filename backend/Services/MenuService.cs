using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItemDto>> GetAllAsync();
        Task<MenuItemDto?> GetByIdAsync(int id);
        Task<MenuItemDto> CreateAsync(CreateMenuItemDto dto);
        Task<MenuItemDto?> UpdateAsync(int id, MenuItemDto dto);
        Task<bool> DeleteAsync(int id);
    }

    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;
        public MenuService(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<MenuItemDto>> GetAllAsync() =>
            await _context.MenuItems.Where(m => m.IsAvailable).Select(m => Map(m)).ToListAsync();

        public async Task<MenuItemDto?> GetByIdAsync(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            return item == null ? null : Map(item);
        }

        public async Task<MenuItemDto> CreateAsync(CreateMenuItemDto dto)
        {
            var item = new MenuItem { Name = dto.Name, Description = dto.Description, Price = dto.Price, ImageUrl = dto.ImageUrl, Category = dto.Category };
            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();
            return Map(item);
        }

        public async Task<MenuItemDto?> UpdateAsync(int id, MenuItemDto dto)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item == null) return null;
            item.Name = dto.Name; item.Description = dto.Description; item.Price = dto.Price;
            item.ImageUrl = dto.ImageUrl; item.Category = dto.Category; item.IsAvailable = dto.IsAvailable;
            await _context.SaveChangesAsync();
            return Map(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item == null) return false;
            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        private static MenuItemDto Map(MenuItem m) => new() { Id = m.Id, Name = m.Name, Description = m.Description, Price = m.Price, ImageUrl = m.ImageUrl, Category = m.Category, IsAvailable = m.IsAvailable };
    }
}
