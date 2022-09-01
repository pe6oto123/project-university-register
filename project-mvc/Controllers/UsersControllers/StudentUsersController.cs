using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace project_mvc.Controllers.UsersControllers
{
	[Authorize(Roles = "Student")]
	public class StudentUsersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
