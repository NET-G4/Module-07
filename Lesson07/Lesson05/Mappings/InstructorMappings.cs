using Lesson05.Entities;
using Lesson05.Models.ViewModels;

namespace Lesson05.Mappings
{
    public static class InstructorMappings
    {
        public static InstructorViewModel ToViewModel(this Instructor instructor)
        {
            return new InstructorViewModel
            {
                Id = instructor.Id,
                FullName = instructor.FirstName + " " + instructor.LastName,
                Email = instructor.Email,
                DepartmentId = instructor.DepartmentId,
                Department = instructor.Department?.Name ?? string.Empty
            };
        }

        public static Instructor ToEntity(this InstructorActionViewModel viewModel)
        {
            return new Instructor
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                DepartmentId = viewModel.DepartmentId,
            };
        }
    }
}
