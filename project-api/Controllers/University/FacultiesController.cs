using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api._Util;
using project_api.Database.Contexts;
using project_api.Database.Entities.University;

namespace project_api.Controllers.University
{
	[Route("api/[controller]")]
	[ApiController]
	public class FacultiesController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public FacultiesController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/Faculties
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculty(string? facultySearch = null, string? searchParam = "FacultyName")
		{
			if (_context.Faculty == null)
			{
				return NotFound();
			}

			IEnumerable<Faculty> faculties = await _context.Faculty
				.Include(s => s.Address)
				.Include(s => s.Address!.City)
				.ToListAsync();

			if (!string.IsNullOrEmpty(facultySearch))
				faculties = faculties.Where(s => HelperClass.GetPropertyValue(s, searchParam!)!.ToString()!.Contains(facultySearch));

			return faculties.ToList();
		}

		// GET: api/Faculties/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Faculty>> GetFaculty(int id)
		{
			if (_context.Faculty == null)
			{
				return NotFound();
			}
			var faculty = await _context.Faculty
				.Include(s => s.Address)
				.Include(s => s.Address!.City)
				.FirstAsync(s => s.Id == id);

			if (faculty == null)
			{
				return NotFound();
			}

			return faculty;
		}

		// PUT: api/Faculties/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutFaculty(int id, Faculty faculty)
		{
			if (id != faculty.Id)
			{
				return BadRequest();
			}

			//_context.Attach(faculty);
			//_context.Entry(faculty.Address!).Reference(s => s.City).IsModified = true;
			//_context.Entry(faculty.Address!).State = EntityState.Modified;
			//_context.Entry(faculty).State = EntityState.Modified;
			_context.Update(faculty);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FacultyExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Faculties
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Faculty>> PostFaculty(Faculty faculty)
		{
			if (_context.Faculty == null)
			{
				return Problem("Entity set 'DatabaseContext.Faculty'  is null.");
			}
			_context.Faculty.Add(faculty);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetFaculty", new { id = faculty.Id }, faculty);
		}

		// DELETE: api/Faculties/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFaculty(int id)
		{
			if (_context.Faculty == null)
			{
				return NotFound();
			}
			var faculty = await _context.Faculty.Include(s => s.Address).FirstAsync(s => s.Id == id);
			if (faculty == null)
			{
				return NotFound();
			}

			_context.Address.Remove(faculty.Address!);
			_context.Faculty.Remove(faculty);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool FacultyExists(int id)
		{
			return (_context.Faculty?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
