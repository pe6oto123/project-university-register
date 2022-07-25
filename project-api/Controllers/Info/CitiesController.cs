using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_api.Database.Contexts;
using project_api.Database.Entities.Location;
using System.Reflection;

namespace project_api.Controllers.Info
{
	[Route("api/[controller]")]
	[ApiController]
	public class CitiesController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public CitiesController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: api/Cities
		[HttpGet]
		public async Task<ActionResult<IEnumerable<City>>> GetCity(string? citySearch = null, string? searchParam = "Name")
		{
			if (_context.City == null)
			{
				return NotFound();
			}

			IEnumerable<City> cities = await _context.City.ToListAsync();
			PropertyInfo? propertyInfo;
			try
			{
				propertyInfo = typeof(City).GetProperty(searchParam!);
			}
			catch (ArgumentNullException)
			{
				propertyInfo = typeof(City).GetProperty("Name");
			}

			if (!string.IsNullOrEmpty(citySearch))
				cities = cities.Where(s => propertyInfo!.GetValue(s)!.ToString()!.Contains(citySearch));

			return cities.ToList();
		}

		// GET: api/Cities/5
		[HttpGet("{id}")]
		public async Task<ActionResult<City>> GetCity(int id)
		{
			if (_context.City == null)
			{
				return NotFound();
			}
			var city = await _context.City.FindAsync(id);

			if (city == null)
			{
				return NotFound();
			}

			return city;
		}

		// PUT: api/Cities/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCity(int id, City city)
		{
			if (id != city.Id)
			{
				return BadRequest();
			}

			_context.Entry(city).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CityExists(id))
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

		// POST: api/Cities
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<City>> PostCity(City city)
		{
			if (_context.City == null)
			{
				return Problem("Entity set 'DatabaseContext.City'  is null.");
			}
			_context.City.Add(city);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCity", new { id = city.Id }, city);
		}

		// DELETE: api/Cities/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCity(int id)
		{
			if (_context.City == null)
			{
				return NotFound();
			}
			var city = await _context.City.FindAsync(id);
			if (city == null)
			{
				return NotFound();
			}

			_context.City.Remove(city);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool CityExists(int id)
		{
			return (_context.City?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
