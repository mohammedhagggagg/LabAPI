using System.ComponentModel.DataAnnotations;

namespace Day01.DTO
{
    public class AddAdminRole
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
