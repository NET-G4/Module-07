using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Lesson05.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<CourseAssignment> Assignments { get; set; }

        public Instructor()
        {
            Assignments = new List<CourseAssignment>();
        }
    }
}
