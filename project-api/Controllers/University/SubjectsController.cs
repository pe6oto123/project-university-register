using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api._Util;
using project_api.Database.Contexts;
using project_api.Database.Entities.University;

namespace project_api.Controllers.University
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectsController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public SubjectsController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/Subjects
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Subject>>> GetSubject(int? facultyId = null, string? subjectSearch = null, string? searchParam = "SubjectName")
		{
			if (_context.Subject == null)
			{
				return NotFound();
			}

			IEnumerable<Subject> subjects = await _context.Subject
				.Include(s => s.Faculty)
				.Where(s => facultyId == null || s.FacultyId == facultyId)
				.ToListAsync();

			if (!string.IsNullOrEmpty(subjectSearch))
				subjects = subjects.Where(s => HelperClass.GetPropertyValue(s, searchParam!)!.ToString()!.Contains(subjectSearch));

			return subjects.ToList();
		}

		// GET: api/Subjects/Schedule
		[HttpGet("Schedule/courseId={courseId}&year={year}")]
		public async Task<ActionResult<IEnumerable<Subject>>> GetSubjectSchedule(int courseId, int year, string? subjectSearch = null)
		{
			if (_context.Subject == null)
			{
				return NotFound();
			}

			var schedules = await _context.Schedule
				.Include(s => s.SchedulesSubjects)
				.Where(s => s.CourseId == courseId)
				.ToListAsync();

			var facultyId = (await _context.Course
				.Include(s => s.Faculty)
				.FirstAsync(s => s.Id == courseId)).FacultyId;

			List<int> subjectIds = new();
			int scheduleId = schedules.Where(s => s.Year == year).FirstOrDefault()!.Id;

			foreach (var schedule in schedules)
				foreach (var subject in schedule.SchedulesSubjects!)
					subjectIds.Add(subject.SubjectId);


			var subjects = await _context.Subject
				.Include(s => s.Faculty)
				.Include(s => s.SchedulesSubjects)
				.Where(s => s.FacultyId == facultyId)
				.Where(s => !s.SchedulesSubjects!.Any() ||
					s.SchedulesSubjects!.Any(x => x.ScheduleId == scheduleId) ||
					s.SchedulesSubjects!.Any(x => !subjectIds.Contains(x.SubjectId)))
				.Where(s => subjectSearch == null || s.SubjectName!.Contains(subjectSearch!))
				.ToListAsync();


			return subjects.ToList();
		}

		// GET: api/Subjects/Faculty
		[HttpGet("Faculty/facultyId={facultyId}")]
		public async Task<ActionResult<IEnumerable<Subject>>> GetSubjectsFaculty(int facultyId, string? subjectSearch = null)
		{
			if (_context.Subject == null)
			{
				return NotFound();
			}

			var subjects = await _context.Subject
				.Where(s => s.FacultyId == facultyId)
				.Where(s => subjectSearch == null ||
					s.SubjectName!.Contains(subjectSearch!))
				.ToListAsync();

			return subjects.ToList();
		}

		// GET: api/Subjects/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Subject>> GetSubject(int id)
		{
			if (_context.Subject == null)
			{
				return NotFound();
			}
			var subject = await _context.Subject
				.Include(s => s.Faculty)
				.FirstAsync(s => s.Id == id);

			if (subject == null)
			{
				return NotFound();
			}

			return subject;
		}

		// PUT: api/Subjects/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutSubject(int id, Subject subject)
		{
			if (id != subject.Id)
			{
				return BadRequest();
			}

			_context.Entry(subject).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!SubjectExists(id))
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

		// POST: api/Subjects
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Subject>> PostSubject(Subject subject)
		{
			if (_context.Subject == null)
			{
				return Problem("Entity set 'DatabaseContext.Subject'  is null.");
			}
			_context.Subject.Add(subject);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetSubject", new { id = subject.Id }, subject);
		}

		// DELETE: api/Subjects/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSubject(int id)
		{
			if (_context.Subject == null)
			{
				return NotFound();
			}
			var subject = await _context.Subject.FindAsync(id);
			if (subject == null)
			{
				return NotFound();
			}

			_context.Subject.Remove(subject);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool SubjectExists(int id)
		{
			return (_context.Subject?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
