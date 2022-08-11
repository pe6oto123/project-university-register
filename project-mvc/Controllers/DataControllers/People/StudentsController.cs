using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.Location;
using project_mvc.Models.DataModels.People;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.DataControllers.People
{
	public class StudentsController : Controller
	{
		private HttpResponseMessage? _response;

		// GET: Students
		public async Task<IActionResult> Index(string studentSearch, string searchParam)
		{
			if (!string.IsNullOrEmpty(studentSearch))
				ViewBag.Search = searchParam;
			else
				ViewBag.Search = "FirstName";

			ViewBag.TeacherSearch = studentSearch;

			_response = await Client.GetClient().GetAsync($"{Client._routeStudents}?studentSearch={studentSearch}&searchParam={searchParam}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var students = await _response.Content.ReadFromJsonAsync<IEnumerable<Student>>();

			return View(students);
		}

		/*// GET: Students/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Student == null)
			{
				return NotFound();
			}

			var student = await _context.Student
				.FirstOrDefaultAsync(m => m.Id == id);
			if (student == null)
			{
				return NotFound();
			}

			return View(student);
		}*/

		// GET: Students/Create
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

			_response = await Client.GetClient().GetAsync($"{Client._routeCourses}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Courses = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<Course>>(),
				"Id", "CourseN.CourseName");

			return View();
		}

		// POST: Students/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,FacultyNumber,AddressId,Address,FacultyId,CourseId")] Student student)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PostAsJsonAsync($"{Client._routeStudents}/", student);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(student);
		}

		// GET: Students/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			_response = await Client.GetClient().GetAsync($"{Client._routeStudents}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var student = await _response.Content.ReadFromJsonAsync<Student>();

			_response = await Client.GetClient().GetAsync($"{Client._routeCities}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Cities = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<City>>(),
				"Id", "CityName", student!.AddressId);

			_response = await Client.GetClient().GetAsync($"{Client._routeFaculties}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Faculties = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<Faculty>>(),
				"Id", "FacultyName", student!.FacultyId);

			_response = await Client.GetClient().GetAsync($"{Client._routeCourses}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Courses = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<Course>>(),
				"Id", "CourseN.CourseName", student!.CourseId);


			return View(student);
		}

		// POST: Students/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,FacultyNumber,AddressId,Address,FacultyId,CourseId")] Student student)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PutAsJsonAsync($"{Client._routeStudents}/{id}", student);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(student);
		}

		// POST: Students/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			_response = await Client.GetClient().DeleteAsync($"{Client._routeStudents}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			return RedirectToAction(nameof(Index));
		}
	}
}
