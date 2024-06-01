using System.ComponentModel;

namespace Lesson05.Models.ViewModels
{
    public class InstructorViewModel
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        public string FullName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
    }
}
