using Lesson02.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson02.Models;

[Table(nameof(Todo))]
public class Todo
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "Should be less than 50 characters.")]
    [MinLength(5, ErrorMessage = "Should be at least 5 characters.")]
    public string Name { get; set; }
    public string Description { get; set; }
    public TodoStatus Status { get; set; } = TodoStatus.NotStarted;

    public ICollection<Task> task { get; } = new List<Task>();

    [DisplayName("Status")]
    public string StatusDisplayValue => GetStatusDisplayValue();
    
    public string GetStatusDisplayValue()
        => Status switch
        {
            TodoStatus.NotStarted => "Not Started",
            TodoStatus.InProgress => "In Progress",
            _ => "Completed"
        };
}
