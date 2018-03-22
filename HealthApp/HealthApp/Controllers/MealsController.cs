using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthApp.Data;
using HealthApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HealthApp.Controllers
{
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MealsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Meals
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var userData = _context.Meals.Where(um => um.User.Id == user.Id);
            if (userData.Count() == 0)
            {
                return RedirectToAction("Create");
            }
            else
            { 
                return View(await _context.Meals
                    .Where(s => s.User == user)
                    .ToListAsync());
            }
        }

        public async Task<IActionResult> ChartDetails()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var chartData = await _context.Meals
                .Where(u => u.User == user)
                .OrderByDescending(d => d.MealId)
                .Take(7)
                .ToListAsync();
            
            return View(chartData);
        }

        public async Task<List<Meal>> MealDetails()
        {
            ModelState.Remove("User");
            ApplicationUser user = await GetCurrentUserAsync();
            var chartData = await _context.Meals
                .Where(u => u.User == user)
                .Take(7)
                .ToListAsync();

            List<Meal> MealReports = new List<Meal>();

            foreach(var m in chartData)
            {
                Meal newReport = new Meal
                {
                    Protein = (int)m.Protein,
                    Carbohydrates = (int)m.Carbohydrates,
                    Sugar = (int)m.Sugar,
                    Sodium = (int)m.Sodium
                };

                MealReports.Add(newReport);
            }
            return MealReports;
        }

        // GET: meals/details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .SingleOrDefaultAsync(m => m.MealId == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MealId,Title,Protein,Carbohydrates,Sugar,Sodium")] Meal meal)
        {
            ModelState.Remove("User");
            var model = new Meal
            {
                MealId = meal.MealId,
                User = await _userManager.GetUserAsync(User),
                Title = meal.Title,
                Protein = meal.Protein,
                Carbohydrates = meal.Carbohydrates,
                Sugar = meal.Sugar,
                Sodium = meal.Sodium
             };


            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Meals", new { id = model.MealId });
            }
            return View(meal);
        }

        // GET: Meals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals.SingleOrDefaultAsync(m => m.MealId == id);
            if (meal == null)
            {
                return NotFound();
            }
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MealId,Title,Protein,Carbohydrates,Sugar,Sodium")] Meal meal)
        {
            ModelState.Remove("User");
            if (id != meal.MealId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.MealId))
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
            return View(meal);
        }

        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .SingleOrDefaultAsync(m => m.MealId == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _context.Meals.SingleOrDefaultAsync(m => m.MealId == id);
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int id)
        {
            return _context.Meals.Any(e => e.MealId == id);
        }
    }
}
