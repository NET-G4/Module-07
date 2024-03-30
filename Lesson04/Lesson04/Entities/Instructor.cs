using System.ComponentModel;

namespace Lesson04.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [DisplayName("Family name")]
        public string LastName { get; set; }
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
