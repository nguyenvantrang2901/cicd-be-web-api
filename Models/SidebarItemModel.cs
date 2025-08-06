using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIs_Manager_Inventory.Models
{
    public class SidebarItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public int SidebarId { get; set; }
        [JsonIgnore]
        public Sidebar Sidebar { get; set; }
    }
}
