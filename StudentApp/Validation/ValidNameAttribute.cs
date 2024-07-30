using System.ComponentModel.DataAnnotations;

namespace StudentApp.Validation
{
    public class ValidNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Assuming Required attribute will handle the null or empty check
            if (value is string name)
            {
                if (name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Name must only contain letters and spaces.");
                }
            }

            return ValidationResult.Success; // Assuming other checks will handle empty or null values
        }
    }
}