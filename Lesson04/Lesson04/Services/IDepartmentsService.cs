using Lesson04.Entities;

namespace Lesson04.Services
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
