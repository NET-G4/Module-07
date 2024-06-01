using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lesson05.Data;
using Lesson05.Entities;
using Lesson05.Services;
using Lesson05.Models.ViewModels;

namespace Lesson05.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly UniversityDbContext _context;

        public InstructorsController(UniversityDbContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index(string? searchString, int? departmentId)
        {
            var query = _context.Instructors
                .Include(x => x.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => 
                    x.FirstName.Contains(searchString) ||
                    x.LastName.Contains(searchString) ||
                    x.Email.Contains(searchString) ||
                    x.Department.Name.Contains(searchString));
            }

            if (departmentId.HasValue)
            {
                query = query.Where(x => x.DepartmentId == departmentId);
            }

            var instructors = await query.Select(x => new InstructorViewModel
            {
                Id = x.Id,
                FullName = x.FirstName + " " + x.LastName,
                Email = x.Email,
                Department = x.Department.Name,
                DepartmentId = x.DepartmentId
            }).ToListAsync();
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

            var instructor = await _context.Instructors
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
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
                var entity = new Instructor
                {
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    Email = instructor.Email,
                    DepartmentId = instructor.DepartmentId,
                };
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.Id == id);
        }
    }
}
