using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.Location;
using project_mvc.Models.DataModels.People;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.DataControllers.People
{
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

			_response = await Client.GetClient().GetAsync($"{Client._routeTeachers}?teacherSearch={teacherSearch}&searchParam={searchParam}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var teachers = await _response.Content.ReadFromJsonAsync<IEnumerable<Teacher>>();

			return View(teachers);
		}

		// GET: Teachers/Details/5
		/*
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Teacher == null)
			{
				return NotFound();
			}

			var teacher = await _context.Teacher
				.FirstOrDefaultAsync(m => m.Id == id);
			if (teacher == null)
			{
				return NotFound();
			}

			return View(teacher);
		}*/

		// GET: Teachers/Create
		public async Task<IActionResult> Create()
		{
			_response = await Client.GetClient().GetAsync($"{Client._routeCities}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Cities = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<City>>(),
				"Id", "CityName");

			_response = await Client.GetClient().GetAsync($"{Client._routeFaculties}");
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
				_response = await Client.GetClient().PostAsJsonAsync($"{Client._routeTeachers}/", teacher);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(teacher);
		}

		// GET: Teachers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			_response = await Client.GetClient().GetAsync($"{Client._routeTeachers}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var teacher = await _response.Content.ReadFromJsonAsync<Teacher>();

			_response = await Client.GetClient().GetAsync($"{Client._routeCities}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Cities = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<City>>(),
				"Id", "CityName", teacher!.Address!.City!.Id);

			_response = await Client.GetClient().GetAsync($"{Client._routeFaculties}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Faculties = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<Faculty>>(),
				"Id", "FacultyName", teacher!.Faculty!.Id);


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
				_response = await Client.GetClient().PutAsJsonAsync($"{Client._routeTeachers}/{id}", teacher);
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
			_response = await Client.GetClient().DeleteAsync($"{Client._routeTeachers}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			return RedirectToAction(nameof(Index));
		}
	}
}
