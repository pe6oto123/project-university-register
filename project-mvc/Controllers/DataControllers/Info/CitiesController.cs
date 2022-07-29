using Microsoft.AspNetCore.Mvc;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.Location;

namespace project_mvc.Controllers.DataControllers.Location
{
	public class CitiesController : Controller
	{
		private const string _apiRoute = "api/Cities";
		private HttpResponseMessage? _response;

		// GET: Cities
		public async Task<IActionResult> Index(string citySearch, string searchParam)
		{
			if (!string.IsNullOrEmpty(searchParam))
				ViewBag.Search = searchParam;
			else
				ViewBag.Search = "CityName";

			ViewBag.CitySearch = citySearch;

			_response = await Client.GetClient().GetAsync($"{_apiRoute}?citySearch={citySearch}&searchParam={searchParam}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

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
		public async Task<IActionResult> Create([Bind("Id,CityName,Region,Population")] City city)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PostAsJsonAsync($"{_apiRoute}/", city);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(city);
		}

		// GET: Cities/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			_response = await Client.GetClient().GetAsync($"{_apiRoute}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var city = await _response.Content.ReadFromJsonAsync<City>();

			return View(city);
		}

		// POST: Cities/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,CityName,Region,Population")] City city)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PutAsJsonAsync($"{_apiRoute}/{id}", city);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(city);
		}

		// POST: Cities/Delete/5
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
