namespace APIs_Manager_Inventory.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Unit {  get; set; }
        public string Category { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; } = string.Empty;
    }
}
