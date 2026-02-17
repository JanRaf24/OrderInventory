using OrderInventory.Data;
using OrderInventory.Models;

namespace OrderInventory.Services.Inventories
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(AppDbContext db, ILogger<InventoryService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void SetStock(string sku, int quantity)
        {
            var item = _db.InventoryList.FirstOrDefault(i => i.Sku == sku);
            if (item == null)
                _db.InventoryList.Add(new Inventory { Sku = sku, Quantity = quantity });
            else
                item.Quantity = quantity;

            _db.SaveChanges();
            _logger.LogInformation("Stock set {@Sku} to {Qty}", sku, quantity);
        }

        public Inventory? GetInventory(string sku) => _db.InventoryList.FirstOrDefault(i => i.Sku == sku);
    }
}
