using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api._Util;
using project_api.Database.Contexts;
using project_api.Database.Entities.People;

namespace project_api.Controllers.People
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public StudentsController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/Students
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Student>>> GetStudent(string? studentSearch = null, string? searchParam = "FirstName")
		{
			if (_context.Student == null)
			{
				return NotFound();
			}

			IEnumerable<Student> students = await _context.Student
				.Include(s => s.Address)
				.Include(s => s.Address!.City)
				.Include(s => s.Faculty)
				.Include(s => s.Course)
				.Include(s => s.Course!.CourseN)
				.ToListAsync();

			if (!string.IsNullOrEmpty(studentSearch))
				students = students.Where(s => HelperClass.GetPropertyValue(s, searchParam!)!.ToString()!.Contains(studentSearch));

			return students.ToList();
		}

		// GET: api/Students/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Student>> GetStudent(int id)
		{
			if (_context.Student == null)
			{
				return NotFound();
			}
			var student = await _context.Student
				.Include(s => s.Address)
				.Include(s => s.Address!.City)
				.Include(s => s.Faculty)
				.Include(s => s.Course)
				.Include(s => s.Course!.CourseN)
				.FirstAsync(s => s.Id == id);

			if (student == null)
			{
				return NotFound();
			}

			return student;
		}

		// PUT: api/Students/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutStudent(int id, Student student)
		{
			if (id != student.Id || student.AddressId != student.Address!.Id)
			{
				return BadRequest();
			}

			_context.Update(student);
			_context.Entry(student).Property(s => s.FacultyNumber).IsModified = false;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!StudentExists(id))
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

		// POST: api/Students
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Student>> PostStudent(Student student)
		{
			if (_context.Student == null)
			{
				return Problem("Entity set 'DatabaseContext.Student'  is null.");
			}

			var faculty = await _context.Faculty.FirstAsync(s => s.Id == student.FacultyId);
			var course = await _context.Course.FirstAsync(s => s.Id == student.CourseId);
			var nextFree = await _context.FacultyNum.FirstAsync(s => s.CourseId == course.Id);

			student.FacultyNumber = $"{course.Enrolment!.Value:yy}" +
				$"{new string('0', 3 - faculty.Id.ToString().Length)}{faculty.Id}" +
				$"{new string('0', 3 - course.Id.ToString().Length)}{course.Id}" +
				$"{new string('0', 4 - nextFree.NextFreeId.ToString().Length)}{nextFree.NextFreeId}";

			++nextFree.NextFreeId;
			_context.Entry(nextFree).State = EntityState.Modified;

			_context.Student.Add(student);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetStudent", new { id = student.Id }, student);
		}

		// DELETE: api/Students/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteStudent(int id)
		{
			if (_context.Student == null)
			{
				return NotFound();
			}
			var student = await _context.Student
				.Include(s => s.Address)
				.FirstAsync(s => s.Id == id);

			if (student == null)
			{
				return NotFound();
			}

			_context.Address.Remove(student.Address!);
			_context.Student.Remove(student);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool StudentExists(int id)
		{
			return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
