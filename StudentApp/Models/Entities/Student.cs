using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models.Entities
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
        public string? Course { get; set; } = string.Empty;
        public string? Address { get; set; }

        [Required(ErrorMessage = "ID Number is required.")]
        [RegularExpression(@"^\d{10}V$|^\d{11}$", ErrorMessage = "ID Number must be 10 digits ending with 'V' or exactly 11 digits.")]
        public string IdNumber { get; set; } = string.Empty;

    }
}