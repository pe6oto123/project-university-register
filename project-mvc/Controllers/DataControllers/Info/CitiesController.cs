using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using project_mvc.ApiClient;
using project_mvc.Database.Entities.Location;

namespace project_mvc.Controllers.DataControllers.Location
{
	public class CitiesController : Controller
	{
		private HttpResponseMessage? _response;

		// GET: Cities
		public async Task<IActionResult> Index(string citySearch, string searchParam)
		{
			if (searchParam != null)
				ViewBag.Search = searchParam;
			else
				ViewBag.Search = "Name";

			ViewBag.CitySearch = citySearch;

			_response = await Client.GetClient().GetAsync($"api/Cities?citySearch={citySearch}&searchParam={searchParam}");
			if (!_response.IsSuccessStatusCode)
				return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

			var cities = await _response.Content.ReadFromJsonAsync<IEnumerable<City>>();

			return View(cities);
		}

		// GET: Cities/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Cities/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Region,Population")] City city)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PostAsJsonAsync($"api/Cities/", city);
				if (!_response.IsSuccessStatusCode)
					return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

				return RedirectToAction(nameof(Index));
			}
			return View(city);
		}

		// GET: Cities/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			_response = await Client.GetClient().GetAsync($"api/Cities/{id}");
			if (!_response.IsSuccessStatusCode)
				return NotFound();

			var city = await _response.Content.ReadFromJsonAsync<City>();

			return View(city);
		}

		// POST: Cities/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Region,Population")] City city)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PutAsJsonAsync($"api/Cities/{id}", city);
				if (!_response.IsSuccessStatusCode)
					return BadRequest();

				return RedirectToAction(nameof(Index));
			}
			return View(city);
		}

		// POST: Cities/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			_response = await Client.GetClient().DeleteAsync($"api/Cities/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(JObject.Parse(await _response.Content.ReadAsStringAsync()).ToString());

			return RedirectToAction(nameof(Index));
		}
	}
}
