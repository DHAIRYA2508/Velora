using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required][MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required][MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required][MaxLength(20)]
        public string Role { get; set; } = "Customer";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
