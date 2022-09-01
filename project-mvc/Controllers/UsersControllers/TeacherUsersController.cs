using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.People;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.UsersControllers
{
	[Authorize(Roles = "Teacher")]
	public class TeacherUsersController : Controller
	{
		private HttpResponseMessage? _response;

		public async Task<IActionResult> Index(string subjectSearch)
		{
			ViewBag.SubjectSearch = subjectSearch;

			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;
			int? teacherId = int.Parse(HttpContext.User.Claims.FirstOrDefault(s => s.Type == "id")!.Value);

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeSubjects}/Teacher/teacherId={teacherId}?subjectSearch={subjectSearch}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var subjects = await _response.Content.ReadFromJsonAsync<IEnumerable<Subject>>();

			return View(subjects);
		}

		public async Task<IActionResult> Students(int subjectId, string studentSearch, string searchParam)
		{
			if (!string.IsNullOrEmpty(studentSearch))
				ViewBag.Search = searchParam;
			else
				ViewBag.Search = "FirstName";

			ViewBag.TeacherSearch = studentSearch;
			ViewBag.SubjectId = subjectId;

			if (ViewBag.SelectList == null)
				ViewBag.SelectList = "[]";

			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeStudents}/Subject/subjectId={subjectId}?studentSearch={studentSearch}&searchParam={searchParam}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var students = await _response.Content.ReadFromJsonAsync<IEnumerable<Student>>();

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeSubjects}/Grades");

			ViewBag.Grades = await _response.Content.ReadFromJsonAsync<IEnumerable<Grade>>();

			return View(students);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Students([FromBody] IEnumerable<StudentsSubjects> studentsSubjects, int subjectId)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			if (ModelState.IsValid)
			{
				_response = await Client.GetClient(token)
					.PutAsJsonAsync($"{Client._routeTeachers}/GradeStudents", studentsSubjects);

				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return Json(new { redirectToUrl = Url.Action("Index", "TeacherUsers") });
			}

			_response = await Client.GetClient(token)
					.GetAsync($"{Client._routeStudents}/Subject/subjectId={subjectId}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var students = await _response.Content.ReadFromJsonAsync<IEnumerable<Student>>();

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeSubjects}/Grades");

			ViewBag.Grades = await _response.Content.ReadFromJsonAsync<IEnumerable<Grade>>();

			return View(students);
		}
	}
}
