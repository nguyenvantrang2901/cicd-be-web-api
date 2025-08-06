namespace APIs_Manager_Inventory.DTO
{
    public class SidebarDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<SidebarItemDTO> Children { get; set; }
    }
    public class SidebarItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Route { get; set;}
        public int SidebarId { get; set; }
    }
}
