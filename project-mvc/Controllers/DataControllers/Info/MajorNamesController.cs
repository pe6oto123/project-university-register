/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_mvc.Database.Contexts;
using project_mvc.Database.Entities.University;

namespace project_mvc.Controllers.DataControllers.Info
{
    public class MajorNamesController : Controller
    {
        private readonly DatabaseContext _context;

        public MajorNamesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: MajorNames
        public async Task<IActionResult> Index()
        {
              return _context.MajorName != null ? 
                          View(await _context.MajorName.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.MajorName'  is null.");
        }

        // GET: MajorNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MajorName == null)
            {
                return NotFound();
            }

            var majorName = await _context.MajorName
                .FirstOrDefaultAsync(m => m.Id == id);
            if (majorName == null)
            {
                return NotFound();
            }

            return View(majorName);
        }

        // GET: MajorNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MajorNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MajorName majorName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(majorName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(majorName);
        }

        // GET: MajorNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MajorName == null)
            {
                return NotFound();
            }

            var majorName = await _context.MajorName.FindAsync(id);
            if (majorName == null)
            {
                return NotFound();
            }
            return View(majorName);
        }

        // POST: MajorNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MajorName majorName)
        {
            if (id != majorName.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(majorName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MajorNameExists(majorName.Id))
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
            return View(majorName);
        }

        // GET: MajorNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MajorName == null)
            {
                return NotFound();
            }

            var majorName = await _context.MajorName
                .FirstOrDefaultAsync(m => m.Id == id);
            if (majorName == null)
            {
                return NotFound();
            }

            return View(majorName);
        }

        // POST: MajorNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MajorName == null)
            {
                return Problem("Entity set 'DatabaseContext.MajorName'  is null.");
            }
            var majorName = await _context.MajorName.FindAsync(id);
            if (majorName != null)
            {
                _context.MajorName.Remove(majorName);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MajorNameExists(int id)
        {
          return (_context.MajorName?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
*/