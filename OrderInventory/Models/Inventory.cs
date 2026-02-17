namespace OrderInventory.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
