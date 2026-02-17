namespace OrderInventory.DTOs
{
    public class InventoryRequest
    {
        public string Sku { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
