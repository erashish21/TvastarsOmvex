using System.ComponentModel.DataAnnotations;

namespace TvastarsOmvex.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [MaxLength(500)]
        public string? ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        // store relative path: /images/products/xxx.jpg
        public string? ImagePath { get; set; }

        // optional datasheet path
        public string? DatasheetPath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
