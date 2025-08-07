namespace APIs_Manager_Inventory.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<OrderItem> Items { get; set; }
        public int? DeliveryId { get; set; }
        public Delivery? Delivery { get; set; }
    }
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
