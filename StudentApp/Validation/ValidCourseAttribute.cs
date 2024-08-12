using System.ComponentModel.DataAnnotations;

namespace StudentApp.Validation
{
    public class ValidCourseAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var course = value as string;
            if (string.IsNullOrEmpty(course))
            {
                return ValidationResult.Success;
            }

            if (course.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid course");
            }
        }
    }
}