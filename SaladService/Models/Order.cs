namespace SaladService.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string KitchenArea { get; set; }
    }
}