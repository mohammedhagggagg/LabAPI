

using Day01.Models;
using Microsoft.EntityFrameworkCore;

namespace Day1.Repository.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private APPDbContext context;
        public StudentRepository(APPDbContext context) => this.context = context;

        public void Add(Student student)
        {
            context.Students.Add(student);
        }

        public void Delete(string id)
        {
            context.Students.Remove(GetById(id));
        }

        public List<Student> GetAll()
        {
            return context.Students.ToList();
        }

        public Student GetById(string id)
        {
            return context.Students.FirstOrDefault(s => s.Id == id);
        }

        public Student GetByName(string name)
        {
            return context.Students.FirstOrDefault(s => s.UserName == name);
        }

       

        public void Update(Student student)
        {
            context.Students.Update(student);
        }

       
    }
    public class APPDbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}
