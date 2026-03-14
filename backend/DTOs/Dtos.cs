using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTOs
{
    // ── Auth ──────────────────────────────────────────────────
    public class RegisterDto
    {
        [Required][MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required][EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required][MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }

    // ── Menu ──────────────────────────────────────────────────
    public class MenuItemDto
    {
        public int Id { get; set; }
        [Required][MaxLength(200)] public string Name { get; set; } = string.Empty;
        [MaxLength(1000)] public string Description { get; set; } = string.Empty;
        [Required][Range(0.01, 99999.99)] public decimal Price { get; set; }
        [MaxLength(500)] public string ImageUrl { get; set; } = string.Empty;
        [Required][MaxLength(100)] public string Category { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
    }

    public class CreateMenuItemDto
    {
        [Required][MaxLength(200)] public string Name { get; set; } = string.Empty;
        [MaxLength(1000)] public string Description { get; set; } = string.Empty;
        [Required][Range(0.01, 99999.99)] public decimal Price { get; set; }
        [MaxLength(500)] public string ImageUrl { get; set; } = string.Empty;
        [Required][MaxLength(100)] public string Category { get; set; } = string.Empty;
    }

    // ── Order ─────────────────────────────────────────────────
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }

    public class OrderItemDto
    {
        public int MenuItemId { get; set; }
        public string? MenuItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateOrderDto
    {
        [Required][MaxLength(200)] public string CustomerName { get; set; } = string.Empty;
        [Required] public List<CreateOrderItemDto> OrderItems { get; set; } = new();
    }

    public class CreateOrderItemDto
    {
        [Required] public int MenuItemId { get; set; }
        [Required][Range(1, 100)] public int Quantity { get; set; }
        [Required][Range(0.01, 99999.99)] public decimal Price { get; set; }
    }

    public class UpdateOrderStatusDto
    {
        [Required] public string Status { get; set; } = string.Empty;
    }
}
