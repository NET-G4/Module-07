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

namespace Lesson05.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsService _departmentsService;

        public DepartmentsController(IDepartmentsService departmentsService)
        {
            _departmentsService = departmentsService;
        }

        // GET: Departments
        public async Task<IActionResult> Index(string? searchString)
        {
            return View(await _departmentsService.GetDepartmentsAsync(searchString));
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return BadRequest("Id cannot be null.");
            }

            var department = await _departmentsService.GetByIdAsync(id.Value);
            if (department is null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                var createdDepartment = await _departmentsService.CreateAsync(department);
                
                return RedirectToAction(nameof(Details), new {id = createdDepartment.Id});
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _departmentsService.GetByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentsService.UpdateAsync(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Department id must be provided.");
            }

            var department = await _departmentsService.GetByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departmentsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _departmentsService.GetByIdAsync(id) != null;
        }
    }
}
