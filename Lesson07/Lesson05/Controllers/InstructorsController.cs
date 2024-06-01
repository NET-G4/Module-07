using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lesson05.Data;
using Lesson05.Models.ViewModels;
using Lesson05.Services;

namespace Lesson05.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly UniversityDbContext _context;
        private readonly IInstructorService _instructorService;

        public InstructorsController(UniversityDbContext context, IInstructorService instructorService)
        {
            _context = context;
            _instructorService = instructorService ?? throw new ArgumentNullException(nameof(instructorService));
        }

        // GET: Instructors
        public async Task<IActionResult> Index(string? searchString, int? departmentId, string sortOrder)
        {
            var instructors = await _instructorService.GetInstructors(departmentId, searchString, sortOrder);

            // TODO use departmentsService to get list of departments
            var departments = await _context.Departments.ToListAsync();
            var selectedDepartment = departments.FirstOrDefault(x => x.Id == departmentId);

            ViewBag.Departments = new SelectList(departments, "Id", "Name", selectedDepartment?.Id);
            ViewBag.Search = searchString;

            return View(instructors);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _instructorService.GetById(id.Value);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorActionViewModel instructor)
        {
            if (ModelState.IsValid) 
            {
                var createdInstructor = await _instructorService.CreateInstructor(instructor);
                return RedirectToAction(nameof(Details), new { id = createdInstructor.Id });
            }

            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InstructorActionViewModel instructor)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _instructorService.UpdateInstructor(instructor);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _instructorService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
