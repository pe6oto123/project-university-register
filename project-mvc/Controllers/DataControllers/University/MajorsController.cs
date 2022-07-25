﻿/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_mvc.Database.Contexts;
using project_mvc.Database.Entities.University;

namespace project_mvc.Controllers.DataControllers.University
{
	public class MajorsController : Controller
	{
		private readonly DatabaseContext _context;

		public MajorsController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: Majors
		public async Task<IActionResult> Index()
		{
			return _context.Major != null ?
						View(await _context.Major.Include(s => s.MajorName).ToListAsync()) :
						Problem("Entity set 'DatabaseContext.Major'  is null.");
		}

		// GET: Majors/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Major == null)
			{
				return NotFound();
			}

			var major = await _context.Major
				.FirstOrDefaultAsync(m => m.Id == id);
			if (major == null)
			{
				return NotFound();
			}

			return View(major);
		}

		// GET: Majors/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Majors/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Enrolment")] Major major)
		{
			if (ModelState.IsValid)
			{
				_context.Add(major);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(major);
		}

		// GET: Majors/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Major == null)
			{
				return NotFound();
			}

			var major = await _context.Major.FindAsync(id);
			if (major == null)
			{
				return NotFound();
			}
			return View(major);
		}

		// POST: Majors/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Enrolment")] Major major)
		{
			if (id != major.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(major);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MajorExists(major.Id))
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
			return View(major);
		}

		// GET: Majors/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Major == null)
			{
				return NotFound();
			}

			var major = await _context.Major
				.FirstOrDefaultAsync(m => m.Id == id);
			if (major == null)
			{
				return NotFound();
			}

			return View(major);
		}

		// POST: Majors/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Major == null)
			{
				return Problem("Entity set 'DatabaseContext.Major'  is null.");
			}
			var major = await _context.Major.FindAsync(id);
			if (major != null)
			{
				_context.Major.Remove(major);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool MajorExists(int id)
		{
			return (_context.Major?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
*/