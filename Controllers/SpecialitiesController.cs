using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseOPR.Database;
using CourseOPR.Models;

namespace CourseOPR.Controllers
{
    public class SpecialitiesController : Controller
    {
        private readonly ApplicationContext _context;

        public SpecialitiesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Specialities
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Speciality.Include(s => s.Faculty);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Specialities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Speciality == null)
            {
                return NotFound();
            }

            var speciality = await _context.Speciality
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(m => m.SpecialityId == id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // GET: Specialities/Create
        public IActionResult Create()
        {
            ViewData["FacultyName"] = new SelectList(_context.Faculty, "FacultyId", "FacultyName");
            return View();
        }

        // POST: Specialities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecialityId,SpecialityName,FacultyId")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                Faculty faculty = await _context.Faculty.FirstOrDefaultAsync(s => s.FacultyId == speciality.FacultyId);
                speciality.Faculty = faculty;
                speciality.FacultyId = faculty.FacultyId;
                _context.Add(speciality);
                Console.WriteLine(speciality.FacultyId);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "FacultyId", "FacultyId", speciality.FacultyId);
            return View(speciality);
        }

        // GET: Specialities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Speciality == null)
            {
                return NotFound();
            }

            var speciality = await _context.Speciality.FindAsync(id);
            if (speciality == null)
            {
                return NotFound();
            }
            Faculty faculty = await _context.Faculty.FirstOrDefaultAsync(s => s.FacultyId == speciality.FacultyId);
            speciality.Faculty = faculty;
            speciality.FacultyId = faculty.FacultyId;
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "FacultyId", "FacultyName", speciality.FacultyId);
            return View(speciality);
        }

        // POST: Specialities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecialityId,SpecialityName,FacultyId")] Speciality speciality)
        {
            if (id != speciality.SpecialityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speciality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialityExists(speciality.SpecialityId))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "FacultyId", "FacultyId", speciality.FacultyId);
            return View(speciality);
        }

        // GET: Specialities/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Speciality == null)
            {
                return NotFound();
            }

            var speciality = await _context.Speciality
                .Include(s => s.Faculty)
                .FirstOrDefaultAsync(m => m.SpecialityId == id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // POST: Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Speciality == null)
            {
                return Problem("Entity set 'ApplicationContext.Speciality'  is null.");
            }
            var speciality = await _context.Speciality.FindAsync(id);
            if (speciality != null)
            {
                _context.Speciality.Remove(speciality);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialityExists(int id)
        {
          return (_context.Speciality?.Any(e => e.SpecialityId == id)).GetValueOrDefault();
        }
    }
}
