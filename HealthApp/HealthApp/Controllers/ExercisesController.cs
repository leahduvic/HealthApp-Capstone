﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthApp.Data;
using HealthApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HealthApp.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExercisesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var userData = _context.Exercises.Where(um => um.User.Id == user.Id);
            if (userData.Count() == 0)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View(await _context.Exercises
                    .Where(us => us.User == user)
                    .OrderByDescending(d => d.Date)
                    .ToListAsync());
            }
        }

        public async Task<List<Exercise>> ChartDetails()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var chartData = await _context.Exercises
                .Where(u => u.User == user)
                .Where(d => Int32.Parse(d.Duration) > 0)
                .OrderByDescending(d => d.Date)
                .Take(7)
                .ToListAsync();

                List<Exercise> ExerciseData = new List<Exercise>();
                    foreach(var exercise in chartData)
                    {
                        Exercise ExerciseReport = new Exercise
                        {
                            Date = exercise.Date,
                            Duration = exercise.Duration,
                            Title = exercise.Title
                        };

                            ExerciseData.Add(ExerciseReport);
                    }
            return ExerciseData;
        }

        // GET: ExerciseChart
        public async Task<IActionResult> ExerciseChart()
        {
            return View(await ChartDetails());
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .SingleOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseId,Title,Duration,Date,Weight,Sets,Reps,RoutineId")] Exercise exercise)
        {
                ModelState.Remove("User");
                // creating a new instance of the Exercise model (calling it simply model) to save to db.
                var model = new Exercise
                {
                    ExerciseId = exercise.ExerciseId,
                    User = await _userManager.GetUserAsync(User),
                    Title = exercise.Title,
                    Duration = exercise.Duration,
                    Date = exercise.Date,
                    Weight = exercise.Weight,
                    Sets = exercise.Sets,
                    Reps = exercise.Reps
                };

            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Exercises", new { id = model.ExerciseId });
            }
            return View();
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,Duration,Date,Title,Weight,Sets,Reps")] Exercise exercise)
        {
            ModelState.Remove("User");
            if (id != exercise.ExerciseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.ExerciseId))
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
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .SingleOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.ExerciseId == id);
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }
    }
}
