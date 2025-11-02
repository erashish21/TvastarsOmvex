using System.ComponentModel.DataAnnotations;

namespace TvastarsOmvex.Models
{
    public class Enquiry
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }   // null if contact form not for a specific product
        public Product? Product { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [MaxLength(30)]
        public string? Phone { get; set; }

        [Required, MaxLength(2000)]
        public string Message { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
