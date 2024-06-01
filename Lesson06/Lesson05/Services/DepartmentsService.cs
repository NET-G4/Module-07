using Lesson05.Data;
using Lesson05.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lesson05.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly UniversityDbContext _context;

        public DepartmentsService(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync(string? searchString)
        {
            var departments = _context.Departments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                departments = departments.Where(x => x.Name.Contains(searchString));
            }

            string queryString = departments.ToQueryString();

            return await departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(x => x.Id == id);
            return department;
        }

        public async Task<Department> CreateAsync(Department department)
        {
            var createdDepartment = await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return createdDepartment.Entity;
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (department is null)
            {
                return;
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }
}
