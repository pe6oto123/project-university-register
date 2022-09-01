using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.Location;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.DataControllers.University
{
	[Authorize(Roles = "Admin")]
	public class FacultiesController : Controller
	{
		private HttpResponseMessage? _response;

		// GET: Faculties
		public async Task<IActionResult> Index(string facultySearch, string searchParam)
		{
			if (!string.IsNullOrEmpty(searchParam))
				ViewBag.Search = searchParam;
			else
				ViewBag.Search = "FacultyName";

			ViewBag.FacultySearch = facultySearch;

			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeFaculties}?facultySearch={facultySearch}&searchParam={searchParam}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var faculties = await _response.Content.ReadFromJsonAsync<IEnumerable<Faculty>>();

			return View(faculties);
		}

		// GET: Faculties/Create
		public async Task<IActionResult> Create()
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCities}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Cities = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<City>>(),
				"Id", "CityName");

			return View();
		}

		// POST: Faculties/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FacultyName,AddressId,Address")] Faculty faculty)
		{
			if (ModelState.IsValid)
			{
				string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

				_response = await Client.GetClient(token)
					.PostAsJsonAsync($"{Client._routeFaculties}/", faculty);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(faculty);
		}

		// GET: Faculties/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeFaculties}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var faculty = await _response.Content.ReadFromJsonAsync<Faculty>();

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeCities}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Cities = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<City>>(),
				"Id", "CityName", faculty!.Address!.CityId);

			return View(faculty);
		}

		// POST: Faculties/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FacultyName,AddressId,Address")] Faculty faculty)
		{
			if (ModelState.IsValid)
			{
				string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

				_response = await Client.GetClient(token)
					.PutAsJsonAsync($"{Client._routeFaculties}/{id}", faculty);
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(faculty);
		}

		// POST: Faculties/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.DeleteAsync($"{Client._routeFaculties}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			return RedirectToAction(nameof(Index));
		}
	}
}
