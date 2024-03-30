using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson05.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        [DisplayName("First name")]
        [MaxLength(255, ErrorMessage = "Last name must be max 255 characters."), 
         MinLength(5, ErrorMessage = "Name must be at least 5 characters.")]
        public string FirstName { get; set; }
        [DisplayName("Family name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => FirstName + " " + LastName;

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [Required]
        public string Email { get; set; }
        
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public virtual ICollection<CourseAssignment> Assignments { get; set; }

        public Instructor()
        {
            Assignments = new List<CourseAssignment>();
        }
    }
}
