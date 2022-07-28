using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.Location;
using project_mvc.Models.DataModels.University;

namespace project_mvc.Controllers.DataControllers.University
{
	public class FacultiesController : Controller
	{
		private const string _apiRoute = "api/Faculties";
		private HttpResponseMessage? _response;

		// GET: Faculties
		public async Task<IActionResult> Index(string facultySearch, string searchParam)
		{
			if (searchParam != null)
				ViewBag.Search = searchParam;
			else
				ViewBag.Search = "FacultyName";

			ViewBag.FacultySearch = facultySearch;

			_response = await Client.GetClient().GetAsync($"{_apiRoute}?facultySearch={facultySearch}&searchParam={searchParam}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var faculties = await _response.Content.ReadFromJsonAsync<IEnumerable<Faculty>>();

			return View(faculties);
		}

		/*// GET: Faculties/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Faculty == null)
			{
				return NotFound();
			}

			var faculty = await _context.Faculty
				.FirstOrDefaultAsync(m => m.Id == id);
			if (faculty == null)
			{
				return NotFound();
			}

			return View(faculty);
		}*/

		// GET: Faculties/Create
		public async Task<IActionResult> Create()
		{
			_response = await Client.GetClient().GetAsync($"api/Cities");
			ViewBag.Cities = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<City>>(), "Id", "CityName");

			return View();
		}

		// POST: Faculties/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FacultyName,AddressId,Address,Address.CityId")] Faculty faculty)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PostAsJsonAsync($"{_apiRoute}/", faculty);
				string test = await _response.Content.ReadAsStringAsync();
				if (!_response.IsSuccessStatusCode)
					return Problem(await _response.Content.ReadAsStringAsync());

				return RedirectToAction(nameof(Index));
			}
			return View(faculty);
		}

		// GET: Faculties/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			_response = await Client.GetClient().GetAsync($"{_apiRoute}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());
			var faculty = await _response.Content.ReadFromJsonAsync<Faculty>();

			_response = await Client.GetClient().GetAsync($"api/Cities/");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			ViewBag.Cities = new SelectList(await _response.Content.ReadFromJsonAsync<IEnumerable<City>>(), "Id", "CityName");

			return View(faculty);
		}

		// POST: Faculties/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FacultyName,AddressId,Address,Address.CityId")] Faculty faculty)
		{
			if (ModelState.IsValid)
			{
				_response = await Client.GetClient().PutAsJsonAsync($"{_apiRoute}/{id}", faculty);
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
			_response = await Client.GetClient().DeleteAsync($"{_apiRoute}/{id}");
			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			return RedirectToAction(nameof(Index));
		}
	}
}
