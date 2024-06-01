using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lesson05.Models.ViewModels;

public class InstructorActionViewModel
{
    public int Id { get; set; }
    [Required,
        MaxLength(255, ErrorMessage = "First name cannot be more than 255 characters."),
        MinLength(3, ErrorMessage = "First name must be at least 3 characters.")]
    [DisplayName("First name")]
    public string FirstName { get; set; }

    [Required,
        MaxLength(255, ErrorMessage = "Last name cannot be more than 255 characters."),
        MinLength(3, ErrorMessage = "Last name must be at least 3 characters.")]
    [DisplayName("Last name")]
    public string LastName { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress, ErrorMessage = "Incorrect email format.")]
    public string Email { get; set; }
    
    [DisplayName("Department")]
    public int DepartmentId { get; set; }
}
