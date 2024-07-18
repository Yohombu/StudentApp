using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models.Entities
{
    public class Student
    {
        [Key]//to assign Id as the primary key
        public int Id { get; set; }
        [Required(ErrorMessage = "Need to fill Required fields")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must only contain letters and spaces.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Need to fill Required fields")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address")]
        public string? Email { get; set; }
        public string? Course { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Need to fill Required fields")]
        [RegularExpression(@"^\d{9}V$|^\d{12}$", ErrorMessage = "ID Number must be 9 digits ending with 'V' or 12 digits.")]
        public string IdNumber { get; set; } = string.Empty;

    }
    public class CreateStudentDto
    {
        [Required(ErrorMessage = "Need to fill Required fields")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must only contain letters and spaces.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Need to fill Required fields")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address")]
        public string? Email { get; set; }
        public string? Course { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Need to fill Required fields")]
        [RegularExpression(@"^\d{9}V$|^\d{12}$", ErrorMessage = "ID Number must be 9 digits ending with 'V' or 12 digits.")]
        public string IdNumber { get; set; } = string.Empty;
    }

    public class UpdateStudentDto
    {
        [Required(ErrorMessage = "Need to fill Required fields")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must only contain letters and spaces.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Need to fill Required fields")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address")]
        public string? Email { get; set; }
        public string? Course { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Need to fill Required fields")]
        [RegularExpression(@"^\d{9}V$|^\d{12}$", ErrorMessage = "ID Number must be 9 digits ending with 'V' or 12 digits.")]
        public string IdNumber { get; set; } = string.Empty;
    }
}