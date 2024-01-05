using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalExaminations.Models;

namespace MedicalExaminations.Controllers
{
    public class MedicalExaminationsController : Controller
    {
        private readonly AppDbContext db;

        public MedicalExaminationsController(AppDbContext context)
        {
            db = context;
        }

        // GET: MedicalExaminations
        public async Task<IActionResult> Index()
        {
            var appDbContext = db.MedicalExaminations.Include(m => m.Animal).Include(m => m.Contract).Include(m => m.VetClinic);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MedicalExaminations/Create
        public IActionResult Create(int animalId)
        {
            ViewBag.AnimalId = animalId;
            ViewData["ContractId"] = new SelectList(db.Contracts, "Id", "Id");
            ViewData["VetClinicId"] = new SelectList(db.Organizations, "Id", "Name");
            return View();
        }

        // POST: MedicalExaminations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalExamination medicalExamination)
        {
            db.MedicalExaminations.Add(medicalExamination);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Animals", medicalExamination.AnimalId);
        }

        // GET: MedicalExaminations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                MedicalExamination medicalExamination = await db.MedicalExaminations.FirstOrDefaultAsync(p => p.Id == id);
                ViewData["AnimalId"] = new SelectList(db.Animals, "Id", "Id", medicalExamination.AnimalId);
                ViewData["ContractId"] = new SelectList(db.Contracts, "Id", "Id", medicalExamination.ContractId);
                ViewData["VetClinicId"] = new SelectList(db.Organizations.ToList(), "Id", "Name", medicalExamination.VetClinicId);
                if (medicalExamination != null) return View(medicalExamination);
          
            }
            return NotFound();
        }

        // POST: MedicalExaminations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicalExamination medicalExamination)
        {
            db.MedicalExaminations.Update(medicalExamination);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Animals", medicalExamination.AnimalId);
        }

        // GET: MedicalExaminations/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                MedicalExamination? medicalExamination = await db.MedicalExaminations.FirstOrDefaultAsync(p => p.Id == id);
                if (medicalExamination != null)
                {
                    db.MedicalExaminations.Remove(medicalExamination);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Animals");
                }
            }
            return NotFound();
        }
    }
}
