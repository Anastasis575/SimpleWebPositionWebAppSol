using Microsoft.EntityFrameworkCore;
using SimpleWebPositionApp.Models;
using SimpleWebPositionApp.Models.Dto;

namespace SimpleWebPositionApp.Data {
    public class ProductDbContext : DbContext {
        public DbSet<Product68> Products68 { get; set; }

        public DbSet<Product64> Products64 { get; set; }

        public DbSet<CodeItem> Codes { get; set; }

        public DbSet<Login> Login { get; set; }

        public DbSet<CensusItem> Census { get; set; }

        public DbSet<SearchBar> SearchBar { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options) {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<CensusItem>().HasKey(nameof(CensusItem.TopCode), nameof(CensusItem.Device));
        }


    }
}
