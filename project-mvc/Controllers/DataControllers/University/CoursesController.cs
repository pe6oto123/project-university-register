using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.DataControllers.University
{
	[Authorize(Roles = "Admin")]
	public class CoursesController : Controller
	{
		private HttpResponseMessage? _response;

		// GET: Courses
		public async Task<IActionResult> Index(string courseSearch, string searchParam)
		{
			if (!string.IsNullOrEmpty(searchParam))
				ViewBag.Search = searchParam;
			else
				ViewBag.Search = "CourseN.CourseName";

			ViewBag.CourseSearch = courseSearch;

			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCourses}?courseSearch={courseSearch}&searchParam={searchParam}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var courses = await _response.Content.ReadFromJsonAsync<IEnumerable<Course>>();

			return View(courses);
		}

		// GET: Courses/Schedule/5
		public async Task<IActionResult> Schedule(int courseId, int year, string subjectSearch)
		{
			ViewBag.SubjectSearch = subjectSearch;

			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeSchedules}/courseId={courseId}&year={year}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var schedule = await _response.Content.ReadFromJsonAsync<Schedule>();

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCourses}/{courseId}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Course = await _response.Content.ReadFromJsonAsync<Course>();

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeSubjects}/Schedule/courseId={courseId}&year={year}?subjectSearch={subjectSearch}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Subjects = await _response.Content.ReadFromJsonAsync<IEnumerable<Subject>>();

			return View(schedule);
		}

		// POST: Schedule
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Schedule([FromBody] Schedule schedule, string? subjectSearch)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			if (ModelState.IsValid)
			{
				_response = await Client.GetClient(token)
					.PutAsJsonAsync($"{Client._routeSchedules}/{schedule.Id}?subjectSearch={subjectSearch}", schedule);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return Json(new { redirectToUrl = Url.Action("Index", "Courses") });
			}

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCourses}/{schedule.CourseId}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Course = await _response.Content.ReadFromJsonAsync<Course>();

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeSubjects}/Schedule/courseId={schedule.CourseId}&year={schedule.Year}?subjectSearch={subjectSearch}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Subjects = await _response.Content.ReadFromJsonAsync<IEnumerable<Subject>>();

			return View(schedule);
		}

		// GET: Courses/Create
		public async Task<IActionResult> Create()
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeFaculties}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Faculties = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<Faculty>>(),
				"Id", "FacultyName");

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCourseN}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.CourseNs = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<CourseN>>(),
				"Id", "CourseName");

			return View();
		}

		// POST: Courses/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,CourseNId,CourseLength,FacultyId,Enrolment")] Course course)
		{
			if (ModelState.IsValid)
			{
				string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

				_response = await Client.GetClient(token)
					.PostAsJsonAsync($"{Client._routeCourses}/", course);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(course);
		}

		// GET: Courses/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCourses}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var course = await _response.Content.ReadFromJsonAsync<Course>();

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeFaculties}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Faculties = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<Faculty>>(),
				"Id", "FacultyName", course!.FacultyId);

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCourseN}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.CourseNs = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<CourseN>>(),
				"Id", "CourseName", course.CourseNId);

			return View(course);
		}

		// POST: Courses/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,CourseNId,CourseLength,FacultyId,Enrolment")] Course course)
		{
			if (ModelState.IsValid)
			{
				string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

				_response = await Client.GetClient(token)
					.PutAsJsonAsync($"{Client._routeCourses}/{id}", course);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(course);
		}

		// POST: Courses/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.DeleteAsync($"{Client._routeCourses}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			return RedirectToAction(nameof(Index));
		}
	}
}
