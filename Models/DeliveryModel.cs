namespace APIs_Manager_Inventory.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public string Destination { get; set; }
        public string Carrier { get; set; }
        public string Status { get; set; } //Pending, InTransit, Delivered
        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }
}
