using Day01.CustomValidation;

namespace Day01.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ManagerName { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
