namespace OrderInventory.Services.Orders
{
    public interface IOrderService
    {
        void placeOrder(string sku, int quantity);
    }
}
