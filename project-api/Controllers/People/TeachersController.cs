using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api._Util;
using project_api.Database.Contexts;
using project_api.Database.Entities.People;

namespace project_api.Controllers.People
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeachersController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public TeachersController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/Teachers
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Teacher>>> GetTeacher(string? teacherSearch = null, string? searchParam = "FirstName")
		{
			if (_context.Teacher == null)
			{
				return NotFound();
			}

			IEnumerable<Teacher> teacher = await _context.Teacher
				.Include(s => s.Address)
				.Include(s => s.Address!.City)
				.Include(s => s.Faculty)
				.ToListAsync();

			if (!string.IsNullOrEmpty(teacherSearch))
				teacher = teacher.Where(s => HelperClass.GetPropertyValue(s, searchParam!)!.ToString()!.Contains(teacherSearch));

			return teacher.ToList();
		}

		// GET: api/Teachers/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Teacher>> GetTeacher(int id)
		{
			if (_context.Teacher == null)
			{
				return NotFound();
			}
			var teacher = await _context.Teacher
				.Include(s => s.Address)
				.Include(s => s.Address!.City)
				.Include(s => s.Faculty)
				.Include(s => s.TeachersSubjects)
				.FirstAsync(s => s.Id == id);


			if (teacher == null)
			{
				return NotFound();
			}

			return teacher;
		}

		// PUT: api/Teachers/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTeacher(int id, Teacher teacher, bool editSubjects = false, string? subjectSearch = null)
		{
			if (id != teacher.Id || teacher.AddressId != teacher.Address!.Id)
			{
				return BadRequest();
			}

			if (editSubjects!)
			{
				_context.RemoveRange(await _context.TeachersSubjects
					.Include(s => s.Subject)
					.Where(s => s.TeacherId == teacher.Id)
					.Where(s => subjectSearch == null ||
						subjectSearch == "undefined" ||
						s.Subject!.SubjectName!.Contains(subjectSearch!))
					.ToListAsync());

				_context.AddRange(teacher.TeachersSubjects!);
			}

			_context.Update(teacher);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TeacherExists(id))
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

		// POST: api/Teachers
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
		{
			if (_context.Teacher == null)
			{
				return Problem("Entity set 'DatabaseContext.Teacher'  is null.");
			}
			_context.Teacher.Add(teacher);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
		}

		// DELETE: api/Teachers/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTeacher(int id)
		{
			if (_context.Teacher == null)
			{
				return NotFound();
			}
			var teacher = await _context.Teacher
				.Include(s => s.Address)
				.FirstAsync(s => s.Id == id);

			if (teacher == null)
			{
				return NotFound();
			}

			_context.Address.Remove(teacher.Address!);
			_context.Teacher.Remove(teacher);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool TeacherExists(int id)
		{
			return (_context.Teacher?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
