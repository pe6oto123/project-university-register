using Microsoft.AspNetCore.Authorization;
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
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<IEnumerable<Student>>> GetStudent(string? studentSearch = null, string? searchParam = "FirstName")
		{
			if (_context.Student == null)
			{
				return NotFound();
			}

			IEnumerable<Student> students = await _context.Student
				.Include(s => s.Gender)
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
		[Authorize(Roles = "Admin, Student")]
		public async Task<ActionResult<Student>> GetStudent(int id)
		{
			if (_context.Student == null)
			{
				return NotFound();
			}
			var student = await _context.Student
				.Include(s => s.Gender)
				.Include(s => s.Address!)
				.ThenInclude(s => s.City)
				.Include(s => s.Faculty)
				.Include(s => s.Course!)
				.ThenInclude(s => s.CourseN)
				.Include(s => s.StudentsSubjects)
				.Include(s => s.StudentsSubjects!)
				.ThenInclude(s => s.Subject)
				.Include(s => s.StudentsSubjects!)
				.ThenInclude(s => s.Subject)
				.Include(s => s.StudentsSubjects!)
				.ThenInclude(s => s.Grade)
				.FirstAsync(s => s.Id == id);

			if (student == null)
			{
				return NotFound();
			}

			return student;
		}

		[HttpGet("Gender")]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<IEnumerable<Gender>>> GetGender()
		{
			if (_context.Student == null)
			{
				return NotFound();
			}

			IEnumerable<Gender> gender = await _context.Gender
				.ToListAsync();

			if (gender == null)
			{
				return NotFound();
			}

			return gender.ToList();
		}

		// GET: api/Students/Subject/subjectId=5
		[HttpGet("Subject/subjectId={subjectId}")]
		[Authorize(Roles = "Teacher")]
		public async Task<ActionResult<IEnumerable<Student>>> GetStudentsSubject(int subjectId, string? studentSearch = null, string? searchParam = "FirstName")
		{
			if (_context.Student == null)
			{
				return NotFound();
			}

			IEnumerable<Student> student = await _context.Student
				.Include(s => s.Faculty)
				.Include(s => s.Gender)
				.Include(s => s.Course)
				.Include(s => s.Course!.CourseN)
				.Include(s => s.StudentsSubjects)
				.Include(s => s.StudentsSubjects!
					.Where(s => s.SubjectId == subjectId))
				.ThenInclude(s => s.Grade)
				.Where(s => s.StudentsSubjects!
					.Any(x => x.SubjectId == subjectId))
				.ToListAsync();

			if (!string.IsNullOrEmpty(studentSearch))
				student = student.Where(s => HelperClass.GetPropertyValue(s, searchParam!)!.ToString()!.Contains(studentSearch));

			if (student == null)
			{
				return NotFound();
			}

			return student.ToList();
		}

		// PUT: api/Students/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<Student>> PostStudent(Student student)
		{
			if (_context.Student == null)
			{
				return Problem("Entity set 'DatabaseContext.Student'  is null.");
			}

			var faculty = await _context.Faculty
				.FirstAsync(s => s.Id == student.FacultyId);

			var course = await _context.Course
				.FirstAsync(s => s.Id == student.CourseId);

			var facultyNum = await _context.FacultyNum
				.FirstAsync(s => s.CourseId == course.Id);

			var schedules = await _context.Schedule
				.Include(s => s.SchedulesSubjects)
				.Where(s => s.CourseId == student.CourseId).ToListAsync();

			student.FacultyNumber = $"{course.Enrolment!.Value:yy}" +
				$"{new string('0', 3 - faculty.Id.ToString().Length)}{faculty.Id}" +
				$"{new string('0', 3 - course.Id.ToString().Length)}{course.Id}" +
				$"{new string('0', 4 - facultyNum.NextFreeId.ToString().Length)}{facultyNum.NextFreeId}";

			++facultyNum.NextFreeId;
			_context.Entry(facultyNum).State = EntityState.Modified;

			var subjects = new List<StudentsSubjects>();
			foreach (var schedule in schedules)
				foreach (var subject in schedule.SchedulesSubjects!)
				{
					subjects.Add(new StudentsSubjects()
					{
						StudentId = student.Id,
						SubjectId = subject.SubjectId,
						CourseId = course.Id,
						Year = schedule.Year,
						GradeId = 1
					});
				}
			student.StudentsSubjects = subjects;

			_context.Student.Add(student);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetStudent", new { id = student.Id }, student);
		}

		// DELETE: api/Students/5
		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
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

			var studentUser = await _context.User
				.Where(s => s.StudentId == id)
				.FirstOrDefaultAsync();
			if (studentUser != null)
				_context.User.Remove(studentUser);


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
