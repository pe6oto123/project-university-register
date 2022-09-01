using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.DataControllers.Info
{
	[Authorize(Roles = "Admin")]
	public class CourseNController : Controller
	{
		private HttpResponseMessage? _response;

		// GET: CourseN
		public async Task<IActionResult> Index(string courseSearch)
		{
			ViewBag.CourseSearch = courseSearch;

			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCourseN}?courseSearch={courseSearch}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var courseN = await _response.Content.ReadFromJsonAsync<IEnumerable<CourseN>>();

			return View(courseN);
		}

		// GET: CourseN/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: CourseN/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,CourseName")] CourseN courseN)
		{
			if (ModelState.IsValid)
			{
				string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

				_response = await Client.GetClient(token)
					.PostAsJsonAsync($"{Client._routeCourseN}/", courseN);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(courseN);
		}

		// GET: CourseN/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCourseN}/{id}");
			if (!_response.IsSuccessStatusCode)
				return NotFound();

			var courseName = await _response.Content.ReadFromJsonAsync<CourseN>();

			return View(courseName);
		}

		// POST: CourseN/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,CourseName")] CourseN courseN)
		{
			if (id != courseN.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

				_response = await Client.GetClient(token)
					.PutAsJsonAsync($"{Client._routeCourseN}/{id}", courseN);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}

			return View(courseN);
		}

		// POST: CourseN/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.DeleteAsync($"{Client._routeCourseN}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			return RedirectToAction(nameof(Index));
		}
	}
}

