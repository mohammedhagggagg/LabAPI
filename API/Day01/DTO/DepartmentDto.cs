using Day01.CustomValidation;
using Day01.Models;

namespace Day01.DTO
{
    public class DepartmentDto
    {
        [UniqeDeptName]
        public string Name { get; set; }
        [LocationValidation]
        public string Location { get; set; }
        public string ManagerName { get; set; }
    }
}
