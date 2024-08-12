using Microsoft.EntityFrameworkCore;
using StudentApp.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models.Entities
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Need to fill Required fields")]
        [ValidName(ErrorMessage = "Name must only contain letters and spaces.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Need to fill Required fields")]
        [CustomEmail(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string? Email { get; set; }

        public string? Course { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Need to fill Required fields")]
        [ValidIdNumber(ErrorMessage = "ID Number must be 9 digits ending with 'V' or 12 digits.")]
        public string IdNumber { get; set; } = string.Empty;
    }

}