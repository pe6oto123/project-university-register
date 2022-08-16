using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project_api.Database.Contexts;
using project_api.Database.Entities.Access;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project_api.Controllers.Access
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly DatabaseContext _context;
		private readonly IConfiguration _configuration;

		public UsersController(DatabaseContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		// POST: api/Users/Login
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost("Login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login(User user)
		{
			if (_context.User == null)
			{
				return Problem("Entity set 'DatabaseContext.Users'  is null.");
			}

			if (User.Identity!.Name == user.UserName)
				return BadRequest();

			try
			{
				user = await _context.User
					.Include(s => s.UserRole)
					.FirstAsync(s => s.UserName == user.UserName && s.Password == user.Password);
			}
			catch (InvalidOperationException) { return BadRequest(); }

			int? id = null;
			id = user.UserRole!.UserRoleName == "Teacher" ? user.TeacherId : user.StudentId;

			var token = Authenticate(id, user.UserName!, user.UserRole!.UserRoleName!);

			return Ok(token);
		}

		// POST: api/Users/Register
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[AllowAnonymous]
		[HttpPost("Register")]
		public async Task<ActionResult<User>> Register(User user)
		{
			if (_context.User == null)
			{
				return Problem("Entity set 'DatabaseContext.Users'  is null.");
			}

			if (user.UserName!.Contains('@'))
			{
				bool isTeacherEx = await _context.User
					.Where(s => s.UserName == user.UserName)
					.AnyAsync();

				if (isTeacherEx)
					return BadRequest();

				int? teacherId = (await _context.Teacher
					.Where(s => s.Email! == user.UserName)
					.FirstOrDefaultAsync())?.Id;

				if (teacherId == null)
					return BadRequest();

				user.TeacherId = teacherId;
				user.UserRoleId = (await _context.UserRole
					.Where(s => s.UserRoleName == "Teacher")
					.FirstAsync()).Id;
			}
			else if (user.UserName!.Contains("student"))
			{
				bool isStudentEx = await _context.User
					.Where(s => s.UserName == user.UserName)
					.AnyAsync();

				if (isStudentEx)
					return BadRequest();

				int? studentId = (await _context.Student
				.Where(s => s.FacultyNumber == user.UserName.Replace("student", ""))
				.FirstOrDefaultAsync())?.Id;

				if (studentId == null)
					return BadRequest();

				user.StudentId = studentId;
				user.UserRoleId = (await _context.UserRole
					.Where(s => s.UserRoleName == "Student")
					.FirstAsync()).Id;

			}
			else
				return BadRequest();

			_context.User.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetUser", new { id = user.Id }, user);
		}

		private string Authenticate(int? id, string userName, string userRole)
		{
			var key = _configuration.GetSection("JwtConfig")["Key"];
			var keyBytes = Encoding.ASCII.GetBytes(key);

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim("id", id.ToString() ?? "null"),
					new Claim(ClaimTypes.Name, userName),
					new Claim(ClaimTypes.Role, userRole)
				}),
				Expires = DateTime.UtcNow.AddMinutes(30),
				SigningCredentials = new SigningCredentials
				(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
