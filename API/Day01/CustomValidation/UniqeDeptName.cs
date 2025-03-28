using Day01.Data;
using System.ComponentModel.DataAnnotations;

namespace Day01.CustomValidation
{
    public class UniqeDeptName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _context = validationContext.GetRequiredService<AppDbContext>();

            string DeptName = value?.ToString();
            if (string.IsNullOrWhiteSpace(DeptName))
                return new ValidationResult("Dept Name Is Required");

            var existed = _context.Departments.FirstOrDefault(d => d.Name == DeptName);
            if (existed != null)
                return new ValidationResult("Dept Name Must be Unique");

            return ValidationResult.Success;
        }
    }
}
