using System.ComponentModel.DataAnnotations;

namespace StudentApp.Validation
{
    public class ValidNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Name is required.");
            }

            var name = value.ToString();
            if (!string.IsNullOrEmpty(name) && name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Name must only contain letters and spaces.");
            }
        }
    }
}