namespace OrderInventory.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
