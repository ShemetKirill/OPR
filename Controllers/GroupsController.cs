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
    public class GroupsController : Controller
    {
        private readonly ApplicationContext _context;

        public GroupsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Group.Include( m=> m.Speciality);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Group == null)
            {
                return NotFound();
            }

            var group = await _context.Group
                .Include(m =>m.Speciality)
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            ViewData["SpecialityId"] = new SelectList(_context.Speciality, "SpecialityId", "SpecialityName");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,GroupNumber,SpecialityId")] Group group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecialityId"] = new SelectList(_context.Speciality, "SpecialityId", "SpecialityId", group.SpecialityId);
            return View(group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Group == null)
            {
                return NotFound();
            }

            var group = await _context.Group.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            Speciality speciality = await _context.Speciality.FirstOrDefaultAsync(s => s.SpecialityId == group.SpecialityId);
            group.Speciality = speciality;
            group.SpecialityId = speciality.SpecialityId;
            ViewData["SpecialityId"] = new SelectList(_context.Speciality, "SpecialityId", "SpecialityName", group.SpecialityId);
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,GroupNumber,SpecialityId")] Group group)
        {
            if (id != group.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(group.GroupId))
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
            ViewData["SpecialityId"] = new SelectList(_context.Speciality, "SpecialityId", "SpecialityId", group.SpecialityId);
            return View(group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Group == null)
            {
                return NotFound();
            }

            var group = await _context.Group
                .Include(m => m.Speciality)
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Group == null)
            {
                return Problem("Entity set 'ApplicationContext.Group'  is null.");
            }
            var group = await _context.Group.FindAsync(id);
            if (group != null)
            {
                _context.Group.Remove(group);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
          return (_context.Group?.Any(e => e.GroupId == id)).GetValueOrDefault();
        }
    }
}
