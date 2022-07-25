using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using project_mvc.ApiClient;
using project_mvc.Database.Entities.University;

namespace project_mvc.Controllers.DataControllers.University
{
	public class SubjectsController : Controller
	{
		private const string _apiRoute = "api/Subjects";
		private HttpResponseMessage? _response;

		// GET: Subjects
		public async Task<IActionResult> Index(string subjectSearch)
		{
			ViewBag.SubjectSearch = subjectSearch;

			_response = await Client.GetClient().GetAsync($"{_apiRoute}?subjectSearch={subjectSearch}");
			if (!_response.IsSuccessStatusCode)
				return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

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
		public async Task<IActionResult> Create([Bind("Id,Name")] Subject subject)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PostAsJsonAsync($"{_apiRoute}/", subject);
				if (!_response.IsSuccessStatusCode)
					return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

				return RedirectToAction(nameof(Index));
			}
			return View(subject);
		}

		// GET: Subjects/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			_response = await Client.GetClient().GetAsync($"{_apiRoute}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

			var subject = await _response.Content.ReadFromJsonAsync<Subject>();

			return View(subject);
		}

		// POST: Subjects/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subject subject)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PutAsJsonAsync($"{_apiRoute}/{id}", subject);
				if (!_response.IsSuccessStatusCode)
					return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

				return RedirectToAction(nameof(Index));
			}
			return View(subject);
		}

		// POST: Subjects/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			_response = await Client.GetClient().DeleteAsync($"{_apiRoute}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

			return RedirectToAction(nameof(Index));
		}
	}
}
