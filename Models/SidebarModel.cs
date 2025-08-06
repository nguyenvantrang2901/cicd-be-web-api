using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIs_Manager_Inventory.Models
{
    public class Sidebar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int HeaderItemId { get; set; }
        public HeaderItem Header { get; set; }
        [JsonIgnore]
        public List<SidebarItem> Children { get; set; }
    }
}
