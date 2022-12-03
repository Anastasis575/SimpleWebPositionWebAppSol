using Microsoft.EntityFrameworkCore;
using SimpleWebPositionApp.Models;

namespace SimpleWebPositionApp.Data {
    public class ProductDbContext : DbContext {
        public DbSet<ProductItem> Products { get; set; }

        public DbSet<CodeItem> Codes { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options) {

        }

        public DbSet<SimpleWebPositionApp.Models.SearchBar> SearchBar { get; set; }
    }
}
