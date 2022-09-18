using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.People;

namespace project_mvc.Controllers.UsersControllers
{
	[Authorize(Roles = "Student")]
	public class StudentUsersController : Controller
	{
		private HttpResponseMessage? _response;
		public async Task<IActionResult> Index()
		{
			string? studentId = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "id")?.Value;
			string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

			_response = await Client.GetClient(token)
				.GetAsync($"{Client._routeStudents}/{studentId}");

			if (!_response.IsSuccessStatusCode)
				return Problem(await _response.Content.ReadAsStringAsync());

			var student = await _response.Content.ReadFromJsonAsync<Student>();

			return View(student);
		}
	}
}
