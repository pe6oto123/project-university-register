using Microsoft.AspNetCore.Mvc;
using project_mvc.ApiClient;
using project_mvc.Models.AnalysisModels.Derived;

namespace project_mvc.Controllers.DataControllers.Analysis
{
	public class GraphsController : Controller
	{
		private HttpResponseMessage? _response;

		// GET: TeachersGraphs
		public async Task<IActionResult> TeachersGraphs()
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeGraphs}/TeachersGraphs");

			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var teachersGraphs = await _response.Content.ReadFromJsonAsync<IEnumerable<TeachersGraph>>();

			return View(teachersGraphs);
		}

		public async Task<IActionResult> CitiesGraphs()
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeGraphs}/CitiesGraphs");

			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var citiesGraphs = await _response.Content.ReadFromJsonAsync<IEnumerable<CitiesGraph>>();

			return View(citiesGraphs);
		}

		public async Task<IActionResult> StudentsGraphs()
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeGraphs}/StudentsGraph");

			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var studentsGraphs = await _response.Content.ReadFromJsonAsync<IEnumerable<StudentsGraph>>();

			return View(studentsGraphs);
		}
	}
}