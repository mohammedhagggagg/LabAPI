using System.ComponentModel.DataAnnotations;

namespace Day01.DTO
{
    public class StudentDTO
    {
        internal string Photo;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public IFormFile Image { get; set; }

        public int DeptId { get; set; }
    }
}
