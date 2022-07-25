using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using project_mvc.ApiClient;
using project_mvc.Database.Entities.University;

namespace project_mvc.Controllers.DataControllers.Info
{
	public class MajorNamesController : Controller
	{
		private const string _apiRoute = "api/MajorNames";
		private HttpResponseMessage? _response;

		// GET: MajorNames
		public async Task<IActionResult> Index(string majorSearch)
		{
			ViewBag.MajorSearch = majorSearch;

			_response = await Client.GetClient().GetAsync($"{_apiRoute}?majorSearch={majorSearch}");
			if (!_response.IsSuccessStatusCode)
				return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

			var majorNames = await _response.Content.ReadFromJsonAsync<IEnumerable<MajorName>>();

			return View(majorNames);
		}

		// GET: MajorNames/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: MajorNames/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name")] MajorName majorName)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PostAsJsonAsync($"{_apiRoute}/", majorName);
				if (!_response.IsSuccessStatusCode)
					return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

				return RedirectToAction(nameof(Index));
			}
			return View(majorName);
		}

		// GET: MajorNames/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			_response = await Client.GetClient().GetAsync($"{_apiRoute}/{id}");
			if (!_response.IsSuccessStatusCode)
				return NotFound();

			var majorName = await _response.Content.ReadFromJsonAsync<MajorName>();

			return View(majorName);
		}

		// POST: MajorNames/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MajorName majorName)
		{
			if (id != majorName.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PutAsJsonAsync($"{_apiRoute}/{id}", majorName);
				if (!_response.IsSuccessStatusCode)
					return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

				return RedirectToAction(nameof(Index));
			}

			return View(majorName);
		}

		// POST: MajorNames/Delete/5
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

