using Lesson05.Entities;

namespace Lesson05.Services
{
    public interface IDepartmentsService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync(string? searchString);
        Task<Department?> GetByIdAsync(int id);
        Task<Department> CreateAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(int id);
    }
}
