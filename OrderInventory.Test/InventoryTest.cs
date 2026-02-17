using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderInventory.Data;
using OrderInventory.Models;
using OrderInventory.Services.Inventories;
using FluentAssertions;

namespace OrderInventory.Tests
{
    public class InventoryTest
    {
        private AppDbContext GetInMemoryDb() => new(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        [Fact]
        public void SetStock_CreatesNewInventory()
        {
            var db = GetInMemoryDb();
            var service = new InventoryService(db, new LoggerFactory().CreateLogger<InventoryService>());

            service.SetStock("X1", 10);

            db.InventoryList.First(i => i.Sku == "X1").Quantity.Should().Be(10);
        }

        [Fact]
        public void SetStock_UpdatesExistingInventory()
        {
            var db = GetInMemoryDb();
            db.InventoryList.Add(new Inventory { Sku = "Y1", Quantity = 5 });
            db.SaveChanges();

            var service = new InventoryService(db, new LoggerFactory().CreateLogger<InventoryService>());
            service.SetStock("Y1", 8);

            db.InventoryList.First(i => i.Sku == "Y1").Quantity.Should().Be(8);
        }

        [Fact]
        public void SetStock_MultipleSkus()
        {
            var db = GetInMemoryDb();
            var service = new InventoryService(db, new LoggerFactory().CreateLogger<InventoryService>());

            service.SetStock("A", 1);
            service.SetStock("B", 2);

            db.InventoryList.First(i => i.Sku == "A").Quantity.Should().Be(1);
            db.InventoryList.First(i => i.Sku == "B").Quantity.Should().Be(2);
        }
    }
}
