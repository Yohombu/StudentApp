using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace StudentApp.Validation
{
    public class CustomEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var email = value.ToString();
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                if (!regex.IsMatch(email))
                {
                    return new ValidationResult("The Email field is not a valid e-mail address.");
                }
            }

            return ValidationResult.Success;
        }
    }
}