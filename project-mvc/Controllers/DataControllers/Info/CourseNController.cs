using Microsoft.AspNetCore.Mvc;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.DataControllers.Info
{
	public class CourseNController : Controller
	{
		private HttpResponseMessage? _response;

		// GET: CourseN
		public async Task<IActionResult> Index(string courseSearch)
		{
			ViewBag.CourseSearch = courseSearch;

			_response = await Client.GetClient().GetAsync($"{Client._routeCourseN}?courseSearch={courseSearch}");
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
				_response = await Client.GetClient().PostAsJsonAsync($"{Client._routeCourseN}/", courseN);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(courseN);
		}

		// GET: CourseN/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			_response = await Client.GetClient().GetAsync($"{Client._routeCourseN}/{id}");
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
				_response = await Client.GetClient().PutAsJsonAsync($"{Client._routeCourseN}/{id}", courseN);
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
			_response = await Client.GetClient().DeleteAsync($"{Client._routeCourseN}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			return RedirectToAction(nameof(Index));
		}
	}
}

