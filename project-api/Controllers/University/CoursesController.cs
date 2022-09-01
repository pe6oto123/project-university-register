using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api._Util;
using project_api.Database.Contexts;
using project_api.Database.Entities.University;

namespace project_api.Controllers.University
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class CoursesController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public CoursesController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/Courses
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Course>>> GetCourse(string? courseSearch = null, string? searchParam = "CourseN.CourseName")
		{
			if (_context.Course == null)
			{
				return NotFound();
			}

			IEnumerable<Course> courses = await _context.Course
				.Include(s => s.CourseN)
				.Include(s => s.Faculty)
				.ToListAsync();

			if (!string.IsNullOrEmpty(courseSearch))
			{
				if (searchParam!.Equals("Enrolment"))
					courses = courses.Where(s => s.Enrolment!.Value.ToString("dd.MM.yyyy г.")!.Contains(courseSearch));
				else
					courses = courses.Where(s => HelperClass.GetPropertyValue(s, searchParam!)!.ToString()!.Contains(courseSearch));
			}
			return courses.ToList();
		}

		// GET: api/Courses/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Course>> GetCourse(int id)
		{
			if (_context.Course == null)
			{
				return NotFound();
			}
			var course = await _context.Course
				.Include(s => s.CourseN)
				.Include(s => s.Faculty)
				.FirstAsync(s => s.Id == id);

			if (course == null)
			{
				return NotFound();
			}

			return course;
		}

		// PUT: api/Courses/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCourse(int id, Course course)
		{
			if (id != course.Id)
			{
				return BadRequest();
			}

			_context.Update(course);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CourseExists(id))
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

		// POST: api/Courses
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Course>> PostCourse(Course course)
		{
			if (_context.Course == null)
			{
				return Problem("Entity set 'DatabaseContext.Course'  is null.");
			}

			_context.Course.Add(course);

			await _context.SaveChangesAsync();

			_context.FacultyNum.Add(new FacultyNum()
			{
				CourseId = course.Id,
				NextFreeId = 1
			});

			for (short i = 0; i < course.CourseLength; i++)
			{
				_context.Schedule.Add(new Schedule()
				{
					Id = 0,
					CourseId = course.Id,
					Year = i + 1
				});
			}

			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCourse", new { id = course.Id }, course);
		}

		// DELETE: api/Courses/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCourse(int id)
		{
			if (_context.Course == null)
			{
				return NotFound();
			}
			var course = await _context.Course.FindAsync(id);
			if (course == null)
			{
				return NotFound();
			}

			_context.Course.Remove(course);
			_context.Schedule.RemoveRange(await _context.Schedule.Where(s => s.CourseId == course.Id).ToListAsync());
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool CourseExists(int id)
		{
			return (_context.Course?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
