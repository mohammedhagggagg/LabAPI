using Day01.Data;
using System.ComponentModel.DataAnnotations;

namespace Day01.CustomValidation
{
    public class LocationValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string Location = value?.ToString();
            if (string.IsNullOrWhiteSpace(Location))
                return new ValidationResult("Dept Location Is Required");

            if (Location == "EG" || Location == "USA") return ValidationResult.Success;
            else return new ValidationResult("Dept Location Must be EG or USA");
        }
    }
}
