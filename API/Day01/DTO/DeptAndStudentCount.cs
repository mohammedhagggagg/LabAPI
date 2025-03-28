using Day01.CustomValidation;

namespace Day01.DTO
{
    public class DeptAndStudentCount
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string ManagerName { get; set; }
        public int Count { get; set; }

        public string Message { get; set; }
        
        public ICollection<ShowStudents> students { get; set; }
    }
}
