using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api.Database.Contexts;
using project_api.Models.Analysis.Derived;

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

		// GET: api/Graphs/TeachersGraphs
		[HttpGet("TeachersGraphs")]
		public async Task<ActionResult<IEnumerable<TeachersGraph>>> GetTeachersGraphs()
		{
			var faculties = await _context.Faculty
				.ToListAsync();

			var teachersGraphs = new List<TeachersGraph>();
			foreach (var faculty in faculties)
			{
				var teachers = await _context.Teacher
					.Where(s => s.FacultyId == faculty.Id)
					.ToListAsync();

				var subjects = await _context.Subject
					.Include(s => s.StudentsSubjects)
					.Where(s => s.FacultyId == faculty.Id)
					.ToListAsync();

				var grades = await _context.StudentsSubjects
					.ToListAsync();

				var teachersSubjectGraph = new List<TeachersSubjectGraph>();
				foreach (var subject in subjects)
				{
					var teachersList = teachers
						.Where(s => subject.StudentsSubjects!
						.Any(x => x.TeacherId == s.Id))
						.ToList();

					teachersSubjectGraph.Add(new TeachersSubjectGraph()
					{
						SubjectId = subject.Id,
						SubjectName = subject.SubjectName,
						TeachersGradeGraph = teachersList.Select(s => new TeachersGradeGraph()
						{
							TeacherFullName = $"{s.FirstName} {s.LastName}",
							AverageGrade = (double?)Math.Round((decimal)grades
							.Where(x => x.SubjectId == subject.Id)
							.Where(x => x.TeacherId == s.Id)
							.Where(x => x.GradeId != 1)
							.Average(c => c.GradeId)!, 2)
						}).ToList()
					});
				}

				teachersGraphs.Add(new TeachersGraph()
				{
					FacultyId = faculty.Id,
					FacultyName = faculty.FacultyName,
					TeachersSubjectGraph = teachersSubjectGraph
				});
			}

			return teachersGraphs;
		}

		// GET: api/Graphs/CitiesGraphs
		[HttpGet("CitiesGraphs")]
		public async Task<ActionResult<IEnumerable<CitiesGraph>>> GetCitiesGraphs()
		{
			var faculties = await _context.Faculty
				.ToListAsync();
			var citiesGraph = new List<CitiesGraph>();

			foreach (var faculty in faculties)
			{
				var students = await _context.Student
					.Include(s => s.Address!.City)
					.Include(s => s.StudentsSubjects)
					.Where(s => s.FacultyId == faculty.Id)
					.ToListAsync();

				var cities = await _context.City
					.ToListAsync();

				cities = cities
					.Where(s => students.Any(x => x.Address.City.Id == s.Id))
					.ToList();

				var citiesGradeGraph = new List<CitiesGradeGraph>();
				foreach (var city in cities)
				{
					double? averageGrade = 0;
					foreach (var student in students.Where(s => s.Address.City.Id == city.Id))
					{
						averageGrade += student.StudentsSubjects!
							.Where(s => s.GradeId != 1)
							.Average(s => s.GradeId);
					}
					averageGrade /= students.Where(s => s.Address.City.Id == city.Id).Count();
					citiesGradeGraph.Add(new CitiesGradeGraph()
					{
						CityName = city.CityName,
						AverageGrade = (double?)Math.Round((decimal)averageGrade!, 2)
					});
				}

				citiesGraph.Add(new CitiesGraph()
				{
					FacultyId = faculty.Id,
					FacultyName = faculty.FacultyName,
					CitiesGradeGraph = citiesGradeGraph
				});
			}

			return citiesGraph;
		}

		// GET: api/Graphs/StudentsGraph
		[HttpGet("StudentsGraph")]
		public async Task<ActionResult<IEnumerable<StudentsGraph>>> GetStudentsGraph()
		{
			var faculties = await _context.Faculty
				.ToListAsync();

			var studentsGraph = new List<StudentsGraph>();
			foreach (var faculty in faculties)
			{
				var students = await _context.Student
					.Include(s => s.Gender)
					.Include(s => s.StudentsSubjects)
					.Where(s => s.FacultyId == faculty.Id)
					.ToListAsync();


				var studentsGenderGraph = new List<StudentsGenderGraph>
				{
					new StudentsGenderGraph()
					{
						StudentsNum = students.Where(s => s.Gender.GenderName == "Male").ToList().Count,
						StudentGender = "Male",
						Color = "#5526d9"
					},
					new StudentsGenderGraph()
					{
						StudentsNum = students.Where(s => s.Gender.GenderName == "Female").ToList().Count,
						StudentGender = "Female",
						Color = "#da0843"
					}
				};

				var grades = await _context.Grade.ToListAsync();
				foreach (var graph in studentsGenderGraph)
				{
					var studentsGradeGraph = new List<StudentsGradeGraph>();
					foreach (var grade in grades.Where(s => s.Id != 1))
					{
						int? gradeDistro = 0;
						foreach (var student in students.Where(s => s.Gender.GenderName == graph.StudentGender))
							gradeDistro += student.StudentsSubjects!
								.Where(s => s.GradeId == grade.Id).Count();

						studentsGradeGraph.Add(new StudentsGradeGraph()
						{
							GradeName = grade.GradeName!,
							GradeNum = gradeDistro
						});
					}

					graph.StudentsGradeGraph = studentsGradeGraph;
				}

				studentsGraph.Add(new StudentsGraph()
				{
					FacultyId = faculty.Id,
					FacultyName = faculty.FacultyName,
					TotalStudents = studentsGenderGraph.Select(s => s.StudentsNum).Sum(),
					StudentsGenderGraph = studentsGenderGraph
				});
			}

			return studentsGraph;
		}
	}
}
