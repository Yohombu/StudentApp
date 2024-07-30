using System.ComponentModel.DataAnnotations;

namespace StudentApp.Validation
{
    public class ValidIdNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("ID Number is required.");
            }

            var idNumber = value.ToString();
            if (idNumber.Length == 10 && idNumber.EndsWith("V") && long.TryParse(idNumber.Substring(0, 9), out _))
            {
                return ValidationResult.Success;
            }
            else if (idNumber.Length == 12 && long.TryParse(idNumber, out _))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("ID Number must be 9 digits ending with 'V' or 12 digits.");
            }
        }
    }
}