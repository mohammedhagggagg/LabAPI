using Day01.Models;


namespace Day1.Repository.StudentRepository
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetById(string id);
        Student GetByName(string name);
        void Add(Student department);
        void Update(Student department);
       
        void Delete(string id);
       
    }
}
