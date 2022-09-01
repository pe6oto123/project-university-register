using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api.Database.Contexts;
using project_api.Database.Entities.University;

namespace project_api.Controllers.Info
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class CourseNController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public CourseNController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/CourseN
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CourseN>>> GetCourseN(string? courseSearch = null)
		{
			if (_context.CourseN == null)
			{
				return NotFound();
			}

			IEnumerable<CourseN> courseN = await _context.CourseN.ToListAsync();

			if (!string.IsNullOrEmpty(courseSearch))
				courseN = courseN.Where(s => s.CourseName!.Contains(courseSearch));

			return courseN.ToList();
		}

		// GET: api/CourseN/5
		[HttpGet("{id}")]
		public async Task<ActionResult<CourseN>> GetCourseN(int id)
		{
			if (_context.CourseN == null)
			{
				return NotFound();
			}
			var courseName = await _context.CourseN.FindAsync(id);

			if (courseName == null)
			{
				return NotFound();
			}

			return courseName;
		}

		// PUT: api/CourseN/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCourseN(int id, CourseN courseName)
		{
			if (id != courseName.Id)
			{
				return BadRequest();
			}

			_context.Entry(courseName).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CourseNExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/CourseN
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<CourseN>> PostCourseN(CourseN courseName)
		{
			if (_context.CourseN == null)
			{
				return Problem("Entity set 'DatabaseContext.CourseN'  is null.");
			}
			_context.CourseN.Add(courseName);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCourseN", new { id = courseName.Id }, courseName);
		}

		// DELETE: api/CourseN/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCourseN(int id)
		{
			if (_context.CourseN == null)
			{
				return NotFound();
			}
			var courseName = await _context.CourseN.FindAsync(id);
			if (courseName == null)
			{
				return NotFound();
			}

			_context.CourseN.Remove(courseName);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool CourseNExists(int id)
		{
			return (_context.CourseN?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
