

using Day01.Data;
using Day01.Models;
using Day1.Repository.DepartmentRepository;

namespace Day01.Repository.DepartmentRepository
{
    public class DepartmentRepository: IDepartmentRepository
    {
        AppDbContext context;
        public DepartmentRepository(AppDbContext _context) 
        {
            context =_context;//= new APPDbContext();
        }

        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }
        public Department GetById(int id)
        {
            return context.Departments.FirstOrDefault(d => d.Id == id);
        }
        public Department GetByName(string name)
        {

            return context.Departments.FirstOrDefault(d => d.Name == name);
        }
        public void Add(Department department) 
        {
            context.Departments.Add(department);
            
        }

        public void Update(Department department)
        {
            context.Departments.Update(department);
        }
        public void UpdateDepartmentName(int id, string name)
        {
            var dept = GetById(id);
            dept.Name = name;
            //context.Departments.Update(dept);
            Update(dept);
        }
        public void Delete(int id)
        {
            context.Departments.Remove(GetById(id));
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
