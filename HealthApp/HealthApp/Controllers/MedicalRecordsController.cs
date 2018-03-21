using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthApp.Data;
using HealthApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HealthApp.Controllers
{
    public class MedicalRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicalRecordsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: MedicalRecords
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            return View(await _context.MedicalRecords
                .Where(m => m.User == user)
                .OrderByDescending(d => d.Date)
                .ToListAsync());
        }

        // GET: MedicalRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = await _context.MedicalRecords
                .SingleOrDefaultAsync(m => m.MedicalRecordId == id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            return View(medicalRecord);
        }

        // GET: MedicalRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedicalRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicalRecordId,Date,RedBloodCount,WhiteBloodCount,BloodGlucose,Cholestorol,Hemoglobin,Iron,B12")] MedicalRecord medicalRecord)
        {
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {

                var modelM = new MedicalRecord
                {
                    MedicalRecordId = medicalRecord.MedicalRecordId,
                    User = await _userManager.GetUserAsync(User),
                    Date = medicalRecord.Date,
                    RedBloodCount = medicalRecord.RedBloodCount,
                    WhiteBloodCount = medicalRecord.WhiteBloodCount,
                    BloodGlucose = medicalRecord.BloodGlucose,
                    Cholestorol = medicalRecord.Cholestorol,
                    Hemoglobin = medicalRecord.Hemoglobin,
                    Iron = medicalRecord.Iron,
                    B12 = medicalRecord.B12
                };

                _context.Add(modelM);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "MedicalRecords", new { id = modelM.MedicalRecordId });
            }
            return View();
        }

        // GET: MedicalRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = await _context.MedicalRecords.SingleOrDefaultAsync(m => m.MedicalRecordId == id);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return View(medicalRecord);
        }

        // POST: MedicalRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicalRecordId,RedBloodCount,WhiteBloodCount,BloodGlucose,Cholestorol,Hemoglobin,Iron,B12")] MedicalRecord medicalRecord)
        {
            ModelState.Remove("User");
            if (id != medicalRecord.MedicalRecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalRecordExists(medicalRecord.MedicalRecordId))
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
            return View(medicalRecord);
        }

        // GET: MedicalRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = await _context.MedicalRecords
                .SingleOrDefaultAsync(m => m.MedicalRecordId == id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            return View(medicalRecord);
        }

        // POST: MedicalRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalRecord = await _context.MedicalRecords.SingleOrDefaultAsync(m => m.MedicalRecordId == id);
            _context.MedicalRecords.Remove(medicalRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalRecordExists(int id)
        {
            return _context.MedicalRecords.Any(e => e.MedicalRecordId == id);
        }
    }
}
