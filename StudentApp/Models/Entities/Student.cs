using System.ComponentModel.DataAnnotations;

namespace StudentApp.Models.Entities
{
    public class Student
    {
        [Key]//to assign Id as the primary key
        public int Id { get; set; }
        [Required]//to make this property not nullable
        public string? Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Course { get; set; } = string.Empty;
        public string? Address { get; set; }

        [Required]
        public string IdNumber { get; set; } = string.Empty;

    }
}