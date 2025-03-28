using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day01.Models
{
    public class Student : IdentityUser
    {
        public string Address { get; set; }
        public string? Image { get; set; }

        [ForeignKey("DepartmentId")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
