namespace OrderInventory.DTOs
{
    public class OrderRequest
    {
        public string Sku { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
