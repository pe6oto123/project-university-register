using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api.Database.Contexts;
using project_api.Models;

namespace project_api.Controllers.Analysis
{
	[Route("api/[controller]")]
	[ApiController]
	public class GraphsController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public GraphsController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/TeachersGraph
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TeachersGraph>>> TeachersGraphs()
		{
			if (_context.Teacher == null)
			{
				return NotFound();
			}

			var teachersGraphs = new List<TeachersGraph>();
			var teachers = await _context.Teacher
				.ToListAsync();
			var subjects = await _context.Subject
				.Include(s => s.StudentsSubjects)
				.ToListAsync();
			var grades = await _context.StudentsSubjects
				.ToListAsync();

			foreach (var subject in subjects)
			{
				var teachersList = teachers
					.Where(s => subject.StudentsSubjects!
					.Any(x => x.TeacherId == s.Id))
					.ToList();

				teachersGraphs.Add(new TeachersGraph()
				{
					SubjectId = subject.Id,
					SubjectName = subject.SubjectName,
					TeachersGradeGraph = teachersList.Select(s => new TeachersGradeGraph()
					{
						TeacherFullName = $"{s.FirstName} {s.LastName}",
						AverageGrade = grades
							.Where(x => x.SubjectId == subject.Id)
							.Where(x => x.TeacherId == s.Id)
							.Where(x => x.GradeId != 1)
							.Average(c => c.GradeId)
					}).ToList()
				});
			}

			return teachersGraphs;
		}
	}
}
