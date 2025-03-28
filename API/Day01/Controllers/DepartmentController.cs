using Day01.Data;
using Day01.DTO;
using Day01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var depts = await _context.Departments.ToListAsync();
            return Ok(depts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var depts = await _context.Departments.FindAsync(id);
            if (depts == null)
                return NotFound();

            return Ok(depts);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult> GetByName(string name)
        {
            var depts = await _context.Departments
                .Where(s => s.Name == name)
                .ToListAsync();

            return depts.Any() ? Ok(depts) : NotFound();
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] DepartmentDto deptDto)
        {
            if (deptDto == null) return BadRequest();

            var department = new Department
            {
                Name = deptDto.Name,
                Location = deptDto.Location,
                ManagerName = deptDto.ManagerName,
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DepartmentDto deptDto)
        {
            if (deptDto == null) return BadRequest();

            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return NotFound();

            dept.Name = deptDto.Name;
            dept.Location = deptDto.Location;
            dept.ManagerName = deptDto.ManagerName;


            _context.Departments.Update(dept);
            await _context.SaveChangesAsync();

            return Ok(dept);
        }

        [HttpPatch("EditName/{id}")]
        public async Task<IActionResult> EditName(int id, [FromQuery] string name)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null)
                return NotFound();

            dept.Name = name;
            _context.Departments.Update(dept);
            await _context.SaveChangesAsync();

            return Ok(dept);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null)
                return NotFound();

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }

        [HttpGet("GetDepartmentsWithStudents")]
        public IActionResult GetDepartmentsWithStudents()
        {
            var departments = _context.Departments
            .Select(d => new DeptAndStudentCount
            {
                Name = d.Name,
                Location = d.Location,
                ManagerName = d.ManagerName,
                Count = d.Students.Count,
                Message = d.Students.Count > 1 ? "OverLoad" : "Not Over",
                students = d.Students.Select(s => new ShowStudents
                {
                    Name = s.UserName,
                    Address = s.Address,
                    Image = s.Image
                }).ToList()
            })
            .ToList();

            return Ok(departments);
        }
    }
}

