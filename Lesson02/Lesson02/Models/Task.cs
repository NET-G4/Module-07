using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lesson02.Models
{
    [Table(nameof(Task))]
    public class Task
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Should be less than 50 characters.")]
        [MinLength(5, ErrorMessage = "Should be at least 5 characters.")]
        public string Name { get; set; }
        public string  Description { get; set; }
        public string  Status { get; set; }
        public DateTime Due_Date { get; set; }
        public int TodoId { get; set; } 
        public Todo Todo { get; set; }
    }
}
