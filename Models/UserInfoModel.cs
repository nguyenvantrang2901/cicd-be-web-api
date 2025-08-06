using System.ComponentModel.DataAnnotations;

namespace APIs_Manager_Inventory.Models
{
    public class UserInfo
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "User name is not empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Name is not empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is not empty")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is not empty")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Gender is not empty")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Avatar is not empty")]
        public string Avatar { get; set; }
        public string Availability { get; set; }
        public string Remark { get; set; }
    }
}
