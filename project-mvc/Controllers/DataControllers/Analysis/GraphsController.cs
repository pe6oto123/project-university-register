using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project_mvc.ApiClient;
using project_mvc.Models.AnalysisModels;

namespace project_mvc.Controllers.DataControllers.Analysis
{
	public class GraphsController : Controller
	{
		private HttpResponseMessage? _response;

		// GET: Graphs
		public async Task<IActionResult> TeachersGraphs()
		{
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeGraphs}");

			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var teachersGraphs = await _response.Content.ReadFromJsonAsync<IEnumerable<TeachersGraph>>();
			ViewBag.Subjects = new SelectList(teachersGraphs, "SubjectId", "SubjectName");


			return View(teachersGraphs);
		}
	}
}