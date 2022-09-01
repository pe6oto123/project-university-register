using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_mvc.ApiClient;
using project_mvc.Models.DataModels.Access;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace project_mvc.Controllers.DataControllers.Access
{
	public class AccountController : Controller
	{
		// GET: Account/ActionForbidden
		public IActionResult Login()
		{
			return RedirectToAction("Login", "Users");
		}
	}

	public class UsersController : Controller
	{
		private HttpResponseMessage? _response;
		// GET: Users/Create
		public IActionResult Register()
		{
			if (User.Identity!.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			return View();
		}

		// POST: Users/Register
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public async Task<IActionResult> Register([Bind("Id,UserName,Password,UserRoleId")] User user)
		{
			if (User.Identity!.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			if (ModelState.IsValid)
			{
				string? token = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "token")?.Value;

				_response = await Client.GetClient(null)
					.PostAsJsonAsync($"{Client._routeUsers}/Register", user);
				if (_response.StatusCode == System.Net.HttpStatusCode.BadRequest)
				{
					TempData["func"] = "showError()";
					return RedirectToAction(nameof(Register));
				}

				return RedirectToAction(nameof(Login));
			}
			return View();
		}

		// GET: Users/Edit/5
		public IActionResult Login()
		{
			if (User.Identity!.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			return View();
		}

		// POST: Users/Login
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public async Task<IActionResult> Login([Bind("Id,UserName,Password,UserRoleId")] User user)
		{
			if (User.Identity!.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			ViewBag.JS_func = null;

			if (ModelState.IsValid)
			{
				_response = await Client.GetClient(null)
					.PostAsJsonAsync($"{Client._routeUsers}/Login", user);

				if (_response.StatusCode == System.Net.HttpStatusCode.BadRequest)
				{
					TempData["func"] = "showError()";
					return RedirectToAction(nameof(Login));
				}

				var token = (await _response.Content.ReadAsStringAsync()).Replace("\"", string.Empty);
				var handler = new JwtSecurityTokenHandler();
				var securityToken = handler.ReadJwtToken(token);

				var claims = new List<Claim>
				{
					new Claim("id", securityToken.Claims.First(s => s.Type == "id").Value),
					new Claim(ClaimTypes.Name, securityToken.Claims.First(s => s.Type == "name").Value),
					new Claim(ClaimTypes.Role, securityToken.Claims.First(s=>s.Type == "role").Value),
					new Claim("token", token)
				};

				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var authProps = new AuthenticationProperties
				{
					ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
					IsPersistent = false,
					IssuedUtc = DateTime.UtcNow,
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					authProps);

				return RedirectToAction(nameof(Index), "Home");
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction(nameof(Index), "Home");
		}
	}
}
