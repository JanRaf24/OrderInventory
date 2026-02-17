using OrderInventory.Data;

namespace OrderInventory.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<OrderService> _logger;

        public OrderService(AppDbContext db, ILogger<OrderService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void placeOrder(string sku, int quantity)
        {
            var item = _db.InventoryList.FirstOrDefault(i => i.Sku == sku);
            if (item == null || item.Quantity < quantity)
                throw new InvalidOperationException("Not enough stock.");

            using var transaction = _db.Database.BeginTransaction();
            try
            {
                item.Quantity -= quantity;
                _db.Orders.Add(new Models.Order { Sku = sku, Quantity = quantity });
                _db.SaveChanges();
                transaction.Commit();
                _logger.LogInformation("Order placed {@Sku} quantity {Qty}", sku, quantity);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
