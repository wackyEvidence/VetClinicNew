using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalExaminations.Models;
using MedicalExaminations.Logging;

namespace MedicalExaminations.Controllers
{
    public class MedicalExaminationsController : Controller
    {
        private readonly AppDbContext db;
        private readonly ActionType createAction;
        private readonly ActionType updateAction;
        private readonly ActionType deleteAction;

        public MedicalExaminationsController(AppDbContext context)
        {
            db = context;
            createAction = db.ActionTypes.Where(at => at.Name == "Create").First();
            updateAction = db.ActionTypes.Where(at => at.Name == "Update").First();
            deleteAction = db.ActionTypes.Where(at => at.Name == "Delete").First();
        }

        public IActionResult Create(int AnimalId)
        {
            ViewBag.AnimalId = AnimalId;
            ViewData["ContractId"] = new SelectList(db.Contracts, "Id", "Display");
            ViewData["VetClinicId"] = new SelectList(db.Organizations, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalExamination medicalExamination)
        {
            medicalExamination.Id = null;
            db.MedicalExaminations.Add(medicalExamination);
            Logger.GetInstance().Log(db, createAction, (int)medicalExamination.Id, medicalExamination.ToJson());
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Animals", new { id = medicalExamination.AnimalId });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                MedicalExamination medicalExamination = await db.MedicalExaminations.FirstOrDefaultAsync(p => p.Id == id);
                ViewData["AnimalId"] = new SelectList(db.Animals, "Id", "Display", medicalExamination.AnimalId);
                ViewData["ContractId"] = new SelectList(db.Contracts, "Id", "Display", medicalExamination.ContractId);
                ViewData["VetClinicId"] = new SelectList(db.Organizations.ToList(), "Id", "Name", medicalExamination.VetClinicId);
                if (medicalExamination != null) return View(medicalExamination);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicalExamination medicalExamination)
        {
            db.MedicalExaminations.Update(medicalExamination);
            Logger.GetInstance().Log(db, updateAction, (int)medicalExamination.Id, medicalExamination.ToJson());
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Animals", new { id = medicalExamination.AnimalId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                MedicalExamination? medicalExamination = await db.MedicalExaminations.FirstOrDefaultAsync(p => p.Id == id);
                if (medicalExamination != null)
                {
                    db.MedicalExaminations.Remove(medicalExamination);
                    Logger.GetInstance().Log(db, deleteAction, (int)medicalExamination.Id);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Animals");
                }
            }
            return NotFound();
        }
    }
}
