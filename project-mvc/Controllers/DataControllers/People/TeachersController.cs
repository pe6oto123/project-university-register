using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.Location;
using project_mvc.Models.DataModels.People;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.DataControllers.People
{
	[Authorize(Roles = "Admin")]
	public class TeachersController : Controller
	{
		private HttpResponseMessage? _response;

		// GET: Teachers
		public async Task<IActionResult> Index(string teacherSearch, string searchParam)
		{
			if (!string.IsNullOrEmpty(searchParam))
				ViewBag.Search = searchParam;
			else
				ViewBag.Search = "FirstName";

			ViewBag.TeacherSearch = teacherSearch;

			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeTeachers}?teacherSearch={teacherSearch}&searchParam={searchParam}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var teachers = await _response.Content.ReadFromJsonAsync<IEnumerable<Teacher>>();

			return View(teachers);
		}

		// GET: Teachers/Details/5
		public async Task<IActionResult> Subjects(int id, string subjectSearch)
		{
			ViewBag.SubjectSearch = subjectSearch;

			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeTeachers}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());
			var teacher = await _response.Content.ReadFromJsonAsync<Teacher>();

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeSubjects}/Faculty/facultyId={teacher!.FacultyId}?subjectSearch={subjectSearch}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Subjects = await _response.Content.ReadFromJsonAsync<IEnumerable<Subject>>();

			return View(teacher);
		}

		// POST: Teachers/Subjects
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Subjects([FromBody] Teacher teacher, string? subjectSearch)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			if (ModelState.IsValid)
			{
				_response = await Client.GetClient(token)
					.PutAsJsonAsync($"{Client._routeTeachers}/{teacher.Id}?editSubjects={true}&subjectSearch={subjectSearch}", teacher);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return Json(new { redirectToUrl = Url.Action("Index", "Teachers") });
			}

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeFaculties}/Subjects/{teacher!.FacultyId}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Subjects = await _response.Content.ReadFromJsonAsync<IEnumerable<Subject>>();

			return View(teacher);
		}


		// GET: Teachers/Create
		public async Task<IActionResult> Create()
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCities}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Cities = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<City>>(),
				"Id", "CityName");

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeFaculties}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Faculties = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<Faculty>>(),
				"Id", "FacultyName");

			return View();
		}

		// POST: Teachers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,AddressId,Address,FacultyId")] Teacher teacher)
		{
			if (ModelState.IsValid)
			{
				string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

				_response = await Client.GetClient(token)
					.PostAsJsonAsync($"{Client._routeTeachers}/", teacher);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(teacher);
		}

		// GET: Teachers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeTeachers}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var teacher = await _response.Content.ReadFromJsonAsync<Teacher>();

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCities}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Cities = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<City>>(),
				"Id", "CityName", teacher!.Address!.CityId);

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeFaculties}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Faculties = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<Faculty>>(),
				"Id", "FacultyName", teacher!.FacultyId);


			return View(teacher);
		}

		// POST: Teachers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,AddressId,Address,FacultyId")] Teacher teacher)
		{
			if (ModelState.IsValid)
			{
				string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

				_response = await Client.GetClient(token)
					.PutAsJsonAsync($"{Client._routeTeachers}/{id}", teacher);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(teacher);
		}


		// POST: Teachers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.DeleteAsync($"{Client._routeTeachers}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			return RedirectToAction(nameof(Index));
		}
	}
}
