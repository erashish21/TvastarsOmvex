
using Microsoft.EntityFrameworkCore;
using TvastarsOmvex.Models;

namespace TvastarsOmvex.Data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
              : base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Project> Projects { get; set; }
       

    }
}
