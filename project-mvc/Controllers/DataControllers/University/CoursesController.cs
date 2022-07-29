using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.DataControllers.University
{
	public class CoursesController : Controller
	{
		private const string _apiRoute = "api/Courses";
		private HttpResponseMessage? _response;

		// GET: Courses
		public async Task<IActionResult> Index(string courseSearch, string searchParam)
		{
			if (!string.IsNullOrEmpty(searchParam))
				ViewBag.Search = searchParam;
			else
				ViewBag.Search = "CourseN.CourseName";

			ViewBag.CourseSearch = courseSearch;

			_response = await Client.GetClient().GetAsync($"{_apiRoute}?courseSearch={courseSearch}&searchParam={searchParam}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var courses = await _response.Content.ReadFromJsonAsync<IEnumerable<Course>>();

			return View(courses);
		}

		/*// GET: Courses/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Course == null)
			{
				return NotFound();
			}

			var course = await _context.Course
				.FirstOrDefaultAsync(m => m.Id == id);
			if (course == null)
			{
				return NotFound();
			}

			return View(course);
		}*/

		// GET: Courses/Create
		public async Task<IActionResult> Create()
		{
			_response = await Client.GetClient().GetAsync($"api/Faculties");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Faculties = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<Faculty>>(), "Id", "FacultyName");

			_response = await Client.GetClient().GetAsync($"api/CourseN");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.CourseNs = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<CourseN>>(), "Id", "CourseName");

			return View();
		}

		// POST: Courses/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,CourseNId,FacultyId,Enrolment")] Course course)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PostAsJsonAsync($"{_apiRoute}/", course);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(course);
		}

		// GET: Courses/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			_response = await Client.GetClient().GetAsync($"{_apiRoute}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var course = await _response.Content.ReadFromJsonAsync<Course>();

			_response = await Client.GetClient().GetAsync($"api/Faculties");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Faculties = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<Faculty>>(),
				"Id", "FacultyName", course!.Faculty!.Id);

			_response = await Client.GetClient().GetAsync($"api/CourseN");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.CourseNs = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<CourseN>>(),
				"Id", "CourseName", course.CourseN!.Id);

			return View(course);
		}

		// POST: Courses/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,CourseNId,FacultyId,Enrolment")] Course course)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PutAsJsonAsync($"{_apiRoute}/{id}", course);
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
			_response = await Client.GetClient().DeleteAsync($"{_apiRoute}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			return RedirectToAction(nameof(Index));
		}
	}
}
