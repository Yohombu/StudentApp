using System.ComponentModel.DataAnnotations;

public class StudentSearchModel
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Course { get; set; }

    public string? Address { get; set; }

    [RegularExpression(@"^\d{10}V$|^\d{11}$", ErrorMessage = "ID Number must be 10 digits ending with 'V' or exactly 11 digits.")]
    public string? IdNumber { get; set; }
}
