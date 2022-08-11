using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api.Database.Contexts;
using project_api.Database.Entities.University;

namespace project_api.Controllers.University
{
	[Route("api/[controller]")]
	[ApiController]
	public class SchedulesController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public SchedulesController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/Schedules/5
		[HttpGet("courseId={courseId}&year={year}")]
		public async Task<ActionResult<Schedule>> GetSchedule(int courseId, int? year)
		{
			if (_context.Schedule == null)
			{
				return NotFound();
			}

			var schedules = await _context.Schedule
				.Include(s => s.SchedulesSubjects)
				.Where(s => s.CourseId == courseId)
				.Where(s => s.Year == year)
				.FirstAsync();

			if (schedules == null)
			{
				return NotFound();
			}

			return schedules;
		}

		// PUT: api/Schedules/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutSchedule(int id, Schedule schedule, string? subjectSearch = null)
		{
			if (id != schedule.Id)
			{
				return BadRequest();
			}

			_context.RemoveRange(await _context.SchedulesSubjects
				.Include(s => s.Subject)
				.Where(s => s.ScheduleId == schedule.Id)
				.Where(s => subjectSearch == null ||
					subjectSearch == "undefined" ||
					s.Subject!.SubjectName!.Contains(subjectSearch!))
				.ToListAsync());

			_context.AddRange(schedule.SchedulesSubjects!);
			_context.Update(schedule);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ScheduleExists(id))
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

		private bool ScheduleExists(int id)
		{
			return (_context.Schedule?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
