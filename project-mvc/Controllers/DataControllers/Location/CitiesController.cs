using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_mvc.Database.Contexts;
using project_mvc.Database.Entities.Location;
using System.Reflection;

namespace project_mvc.Controllers.DataControllers.Location
{
	public class CitiesController : Controller
	{
		private readonly DatabaseContext _context;

		public CitiesController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: Cities
		public async Task<IActionResult> Index(string citySearch, string searchParam)
		{
			IEnumerable<City> cities = await _context.City.ToListAsync();

			PropertyInfo? propertyInfo;
			try
			{
				propertyInfo = typeof(City).GetProperty(searchParam);
			}
			catch (ArgumentNullException)
			{
				propertyInfo = typeof(City).GetProperty("Name");
			}

			ViewBag.CitySearch = citySearch;
			if (!string.IsNullOrEmpty(citySearch))
				cities = cities.Where(s => propertyInfo!.GetValue(s)!.ToString()!.Contains(citySearch));

			return cities != null ?
						View(cities) :
						Problem("Entity set 'DatabaseContext.City'  is null.");
		}

		// GET: Cities/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.City == null)
			{
				return NotFound();
			}

			var city = await _context.City
				.FirstOrDefaultAsync(m => m.Id == id);
			if (city == null)
			{
				return NotFound();
			}

			return View(city);
		}

		// GET: Cities/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Cities/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Region,Population")] City city)
		{
			if (ModelState.IsValid)
			{
				_context.Add(city);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(city);
		}

		// GET: Cities/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.City == null)
			{
				return NotFound();
			}

			var city = await _context.City.FindAsync(id);
			if (city == null)
			{
				return NotFound();
			}
			return View(city);
		}

		// POST: Cities/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Region,Population")] City city)
		{
			if (id != city.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(city);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CityExists(city.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(city);
		}

		// GET: Cities/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.City == null)
			{
				return NotFound();
			}

			var city = await _context.City
				.FirstOrDefaultAsync(m => m.Id == id);
			if (city == null)
			{
				return NotFound();
			}

			return View(city);
		}

		// POST: Cities/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.City == null)
			{
				return Problem("Entity set 'DatabaseContext.City'  is null.");
			}
			var city = await _context.City.FindAsync(id);
			if (city != null)
			{
				_context.City.Remove(city);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CityExists(int id)
		{
			return (_context.City?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
