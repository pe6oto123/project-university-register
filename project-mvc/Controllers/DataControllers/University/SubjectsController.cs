using Microsoft.AspNetCore.Mvc;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.DataControllers.University
{
	public class SubjectsController : Controller
	{
		private HttpResponseMessage? _response;

		// GET: Subjects
		public async Task<IActionResult> Index(string subjectSearch)
		{
			ViewBag.SubjectSearch = subjectSearch;

			_response = await Client.GetClient().GetAsync($"{Client._routeSubjects}?subjectSearch={subjectSearch}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var subject = await _response.Content.ReadFromJsonAsync<IEnumerable<Subject>>();

			return View(subject);
		}

		// GET: Subjects/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Subjects/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,SubjectName")] Subject subject)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PostAsJsonAsync($"{Client._routeSubjects}/", subject);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(subject);
		}

		// GET: Subjects/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			_response = await Client.GetClient().GetAsync($"{Client._routeSubjects}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var subject = await _response.Content.ReadFromJsonAsync<Subject>();

			return View(subject);
		}

		// POST: Subjects/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectName")] Subject subject)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PutAsJsonAsync($"{Client._routeSubjects}/{id}", subject);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(subject);
		}

		// POST: Subjects/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			_response = await Client.GetClient().DeleteAsync($"{Client._routeSubjects}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			return RedirectToAction(nameof(Index));
		}
	}
}
