﻿using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using MedicalExaminations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MedicalExaminations.Controllers
{
    public class ReportsController : Controller
    {
        AppDbContext db;

        public ReportsController(AppDbContext context)
        {
            db = context;
        }

        public ActionResult Index()
        {
            ViewBag.CanViewAnimalsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanViewAnimalsRegistry;
            ViewBag.CanViewOrganizationsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanViewOrganizationsRegistry;
            ViewBag.CanViewContractsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanViewContractsRegistry;
            ViewBag.CanEditReportsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanEditReportsRegistry;
            return View();
        }

        public ActionResult Details(DateOnly dateStart, DateOnly dateEnd)
        {
            Report report = new Report();
            report.StartingDate = dateStart;
            report.EndingDate = dateEnd;
            ViewBag.startingDate = report.StartingDate;
            ViewBag.endingDate = report.EndingDate; 

            var listContracts = db.Contracts
                .Include(a => a.ContractLocations)
                .Include(a => a.MedicalExaminations)
                .ToList();
            var listLocations = db.Locations.ToList();
            double sum = 0;
            foreach (var location in listLocations)
            {
                foreach (var contract in listContracts)
                {
                    if (contract.ContractLocations.Count != 0 && contract.MedicalExaminations.Count!=0)
                    {
                        foreach (var locationId in contract.ContractLocations)
                        {
                            if (locationId.LocationId == location.Id && location.Name == "Тюмень")
                            {
                                sum = sum + locationId.ExaminationCost;
                            }
                        }
                    }
                }
            }
            ViewBag.sum = sum;  
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    if (id != null)
        //    {
        //        Animal? animal = await db.Animals.FirstOrDefaultAsync(p => p.Id == id);
        //        if (animal != null)
        //        {
        //            db.Animals.Remove(animal);
        //            await db.SaveChangesAsync();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return NotFound();
        //}
    }
}
