using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GisButtons.Models;

namespace GisButtons.Controllers
{
    public class CitiesController : Controller
    {
        private readonly CityContext _context;

        public CitiesController(CityContext context)
        {
            _context = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(await _context.City.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(long? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .SingleOrDefaultAsync(m => m.Id == Id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,People,Coord,Photo,Video")] City city)
        {
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(long? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var city = await _context.City.SingleOrDefaultAsync(m => m.Id == Id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long Id, [Bind("Id,Name,People,Coord,Photo,Video")] City city)
        {
            if (Id != city.Id)
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
                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(long? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .SingleOrDefaultAsync(m => m.Id == Id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long Id)
        {
            var city = await _context.City.SingleOrDefaultAsync(m => m.Id == Id);
            _context.City.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CityExists(long Id)
        {
            return _context.City.Any(e => e.Id == Id);
        }

        public IActionResult Textbox(char? letter)
        {

            IQueryable<City> cityList = _context.City;
            if (letter != null)
            {
                cityList = cityList.Where(p => p.Name.First<char>() == letter);
            }
            return View(cityList.ToList());
        }

        public IActionResult Map(int? people, string moreless)
        {
              IQueryable<City> cityList = _context.City;
            if ((people != null) && (moreless != null))
            {
                if (moreless == "Больше")
                    cityList = cityList.Where(p => p.People > people);
                else
                if (moreless == "Меньше")
                    cityList = cityList.Where(p => p.People < people);
                else
                if (moreless == "Равно")
                    cityList = cityList.Where(p => p.People == people);


            }
            string locations = "[";
            foreach (var city in cityList)
            {
                int loc = city.Coord.IndexOf(',');

                locations += "{";
                locations += string.Format("lat: {0}, ", city.Coord.Substring(0, 9));
                locations += string.Format("lng: {0}", city.Coord.Substring(10, 9));
                locations += "}, ";
            }
            locations.TrimEnd(',');
            locations += "]";
            ViewBag.Locations = locations;
            return View();
        }
    }
}


