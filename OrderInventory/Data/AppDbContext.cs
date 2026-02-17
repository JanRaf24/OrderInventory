using Microsoft.EntityFrameworkCore;
using OrderInventory.Models;

namespace OrderInventory.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Inventory> InventoryList => Set<Inventory>();
    }
}
