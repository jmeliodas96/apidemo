using apidemo.Models;
using Microsoft.EntityFrameworkCore;
using apidemo.Controllers.Configurations;

namespace apidemo.Controllers
{
    public class BakeryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source=Bakery.db");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration()).Seed();
        }

    }
}