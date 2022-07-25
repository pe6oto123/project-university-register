using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api.Database.Contexts;
using project_api.Database.Entities.University;

namespace project_api.Controllers.Info
{
	[Route("api/[controller]")]
	[ApiController]
	public class MajorNamesController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public MajorNamesController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/MajorNames
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MajorName>>> GetMajorName(string? majorSearch = null)
		{
			if (_context.MajorName == null)
			{
				return NotFound();
			}

			IEnumerable<MajorName> majorNames = await _context.MajorName.ToListAsync();

			if (!string.IsNullOrEmpty(majorSearch))
				majorNames = majorNames.Where(s => s.Name!.Contains(majorSearch));

			return majorNames.ToList();
		}

		// GET: api/MajorNames/5
		[HttpGet("{id}")]
		public async Task<ActionResult<MajorName>> GetMajorName(int id)
		{
			if (_context.MajorName == null)
			{
				return NotFound();
			}
			var majorName = await _context.MajorName.FindAsync(id);

			if (majorName == null)
			{
				return NotFound();
			}

			return majorName;
		}

		// PUT: api/MajorNames/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutMajorName(int id, MajorName majorName)
		{
			if (id != majorName.Id)
			{
				return BadRequest();
			}

			_context.Entry(majorName).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!MajorNameExists(id))
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

		// POST: api/MajorNames
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<MajorName>> PostMajorName(MajorName majorName)
		{
			if (_context.MajorName == null)
			{
				return Problem("Entity set 'DatabaseContext.MajorName'  is null.");
			}
			_context.MajorName.Add(majorName);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetMajorName", new { id = majorName.Id }, majorName);
		}

		// DELETE: api/MajorNames/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMajorName(int id)
		{
			if (_context.MajorName == null)
			{
				return NotFound();
			}
			var majorName = await _context.MajorName.FindAsync(id);
			if (majorName == null)
			{
				return NotFound();
			}

			_context.MajorName.Remove(majorName);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool MajorNameExists(int id)
		{
			return (_context.MajorName?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
