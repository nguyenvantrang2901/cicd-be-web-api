using System.ComponentModel.DataAnnotations;

namespace APIs_Manager_Inventory.Models
{
    public class HeaderItem
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is not empty")]
        public string Name { get; set; }
        public string? Icon { get; set; }

    }
}
