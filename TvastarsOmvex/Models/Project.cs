using System;
using System.ComponentModel.DataAnnotations;

namespace TvastarsOmvex.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(500)]
        public string? ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        // Image file path, stored in /images/projects/
        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
