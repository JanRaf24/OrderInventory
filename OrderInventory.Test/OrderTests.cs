using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderInventory.Data;
using OrderInventory.Models;
using OrderInventory.Services.Orders;

namespace OrderInventory.Test
{
    public class OrderTests
    {
        private (AppDbContext Db, SqliteConnection Connection) GetSqliteInMemoryDb()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();

            return (context, connection);
        }

        [Fact]
        public void PlaceOrder_DecrementsStock()
        {
            var (db, connection) = GetSqliteInMemoryDb();

            try
            {
                db.InventoryList.Add(new Inventory { Sku = "A1", Quantity = 5 });
                db.SaveChanges();

                var service = new OrderService(
                    db,
                    new LoggerFactory().CreateLogger<OrderService>());

                service.placeOrder("A1", 2);

                db.InventoryList.First(i => i.Sku == "A1")
                    .Quantity.Should().Be(3);
            }
            finally
            {
                db.Dispose();
                connection.Dispose();
            }
        }

        [Fact]
        public void PlaceOrder_Throws_WhenStockNotEnough()
        {
            var (db, connection) = GetSqliteInMemoryDb();

            try
            {
                db.InventoryList.Add(new Inventory { Sku = "B1", Quantity = 1 });
                db.SaveChanges();

                var service = new OrderService(
                    db,
                    new LoggerFactory().CreateLogger<OrderService>());

                Assert.Throws<InvalidOperationException>(
                    () => service.placeOrder("B1", 2));
            }
            finally
            {
                db.Dispose();
                connection.Dispose();
            }
        }

        [Fact]
        public void PlaceOrder_CreatesOrderEntry()
        {
            var (db, connection) = GetSqliteInMemoryDb();

            try
            {
                db.InventoryList.Add(new Inventory { Sku = "C1", Quantity = 3 });
                db.SaveChanges();

                var service = new OrderService(
                    db,
                    new LoggerFactory().CreateLogger<OrderService>());

                service.placeOrder("C1", 2);

                db.Orders.Count().Should().Be(1);
                db.Orders.First().Quantity.Should().Be(2);
            }
            finally
            {
                db.Dispose();
                connection.Dispose();
            }
        }
    }
}
