using Lesson05.Models.ViewModels;

namespace Lesson05.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorViewModel>> GetInstructors(
            int? departmentId,
            string? search,
            string? sort);
        Task<InstructorViewModel?> GetById(int id);
        Task<InstructorViewModel> CreateInstructor(InstructorActionViewModel viewModel);
        Task UpdateInstructor(InstructorActionViewModel viewModel);
        Task Delete(int id);
    }
}
