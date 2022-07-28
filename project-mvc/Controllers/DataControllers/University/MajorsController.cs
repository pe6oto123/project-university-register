/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_mvc.Database.Contexts;
using project_mvc.Database.Entities.University;

namespace project_mvc.Controllers.DataControllers.University
{
	public class CoursesController : Controller
	{
		private readonly DatabaseContext _context;

		public CoursesController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: Courses
		public async Task<IActionResult> Index()
		{
			return _context.Course != null ?
						View(await _context.Course.Include(s => s.CourseName).ToListAsync()) :
						Problem("Entity set 'DatabaseContext.Course'  is null.");
		}

		// GET: Courses/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Course == null)
			{
				return NotFound();
			}

			var major = await _context.Course
				.FirstOrDefaultAsync(m => m.Id == id);
			if (major == null)
			{
				return NotFound();
			}

			return View(major);
		}

		// GET: Courses/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Courses/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Enrolment")] Course major)
		{
			if (ModelState.IsValid)
			{
				_context.Add(major);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(major);
		}

		// GET: Courses/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Course == null)
			{
				return NotFound();
			}

			var major = await _context.Course.FindAsync(id);
			if (major == null)
			{
				return NotFound();
			}
			return View(major);
		}

		// POST: Courses/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Enrolment")] Course major)
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
					if (!CourseExists(major.Id))
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

		// GET: Courses/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Course == null)
			{
				return NotFound();
			}

			var major = await _context.Course
				.FirstOrDefaultAsync(m => m.Id == id);
			if (major == null)
			{
				return NotFound();
			}

			return View(major);
		}

		// POST: Courses/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Course == null)
			{
				return Problem("Entity set 'DatabaseContext.Course'  is null.");
			}
			var major = await _context.Course.FindAsync(id);
			if (major != null)
			{
				_context.Course.Remove(major);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CourseExists(int id)
		{
			return (_context.Course?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
*/