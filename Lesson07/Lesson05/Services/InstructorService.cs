using Lesson05.Data;
using Lesson05.Exceptions;
using Lesson05.Mappings;
using Lesson05.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Lesson05.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly UniversityDbContext _context;

        public InstructorService(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstructorViewModel>> GetInstructors(int? departmentId, string? search, string? sort)
        {
            var query = _context.Instructors
                .Include(x => x.Department)
                .AsQueryable();

            if (departmentId.HasValue)
            {
                query = query.Where(x => x.DepartmentId == departmentId);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(
                    x => x.FirstName.Contains(search) ||
                    x.LastName.Contains(search) ||
                    x.Email.Contains(search) ||
                    x.Department.Name.Contains(search));
            }

            query = sort switch
            {
                "name_desc" => query.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName),
                "email_asc" => query.OrderBy(x => x.Email),
                "email_desc" => query.OrderByDescending(x => x.Email),
                _ => query.OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
            };

            var instructors = await query.Select(x => x.ToViewModel()).ToListAsync();

            return instructors;
        }

        public async Task<InstructorViewModel?> GetById(int id)
        {
            var entity = await _context.Instructors
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new EntityNotFounException($"Instructor with id: {id} does not exist.");
            }

            return entity.ToViewModel();
        }

        public async Task<InstructorViewModel> CreateInstructor(InstructorActionViewModel viewModel)
        {
            var entity = viewModel.ToEntity();
            var createdEntity = _context.Instructors.Add(entity);
            await _context.SaveChangesAsync();
            createdEntity.Entity.Department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == entity.DepartmentId);

            return createdEntity.Entity.ToViewModel();
        }

        public async Task UpdateInstructor(InstructorActionViewModel viewModel)
        {
            var entityToUpdate = await _context.Instructors.FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            try
            {
                _context.Instructors.Update(entityToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!EntityExists(viewModel.Id))
                {
                    throw new EntityNotFounException($"Instructor with id: {viewModel.Id} does not exist.");
                }
            }
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Instructors.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new EntityNotFounException($"Instructor with id: {id} does not exist.");
            }

            _context.Instructors.Remove(entity);
            await _context.SaveChangesAsync();
        }

        private bool EntityExists(int id) => _context.Instructors.Any(x => x.Id == id);
    }
}
