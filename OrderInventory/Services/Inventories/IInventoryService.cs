using OrderInventory.Models;

namespace OrderInventory.Services.Inventories
{
    public interface IInventoryService
    {
        void SetStock(string sku, int quantity);
        Inventory? GetInventory(string sku);
    }
}
