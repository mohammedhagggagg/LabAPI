

using Day01.Models;

namespace Day1.Repository.DepartmentRepository
{
    public interface IDepartmentRepository
    {
      List<Department> GetAll();
        Department GetById(int id);
        Department GetByName(string name);
        void Add(Department department);
        void Update(Department department);
        void UpdateDepartmentName(int id, string name);
        void Delete(int id );
        void Save();
    }
}
