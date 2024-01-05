using DocumentFormat.OpenXml.Office2010.Excel;
using MedicalExaminations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MedicalExaminations.Controllers
{
    public class ContractsController : Controller
    {
        AppDbContext db;

        public ContractsController(AppDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
           ViewBag.CanEditContractsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanEditOrganizationsRegistry;
            return View(await db.Contracts
                .Include(a => a.Client)
                .Include(a => a.Executor)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Clients = new SelectList(db.Organizations.ToList(), "Id", "Name");
            ViewBag.Executors = new SelectList(db.Organizations.ToList(), "Id", "Name");
            ViewBag.Locations = new SelectList(db.Locations.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contract contract, List<int> locs, List<int> costs)
        {
            db.Contracts.Add(contract);
            var ContractLocations = new List<ContractLocation>();
            for (int i = 0; i < locs.Count; i++)
            {
                ContractLocation? contractLocation = new ContractLocation();
                contractLocation.ContractId = contract.Id;
                contractLocation.LocationId = locs[i];
                contractLocation.ExaminationCost = costs[i];
                ContractLocations.Add(contractLocation);
            }
            contract.ContractLocations = ContractLocations;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Clients = new SelectList(db.Organizations.ToList(), "Id", "Name");
            ViewBag.Executors = new SelectList(db.Organizations.ToList(), "Id", "Name");
            ViewBag.Locations = new SelectList(db.Locations.ToList(), "Id", "Name");
            ViewBag.CanEditContractsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanEditContractsRegistry;
            if (id != null)
            {
                Contract? contract = await db.Contracts.Include(c => c.ContractLocations).FirstOrDefaultAsync(p => p.Id == id);
                if (contract != null) return View(contract);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contract contract, List<int> locs, List<string> costs)
        {
            db.Contracts.Update(contract);
            Contract? contractUpdate = await db.Contracts.Include(c => c.ContractLocations).FirstOrDefaultAsync(p => p.Id == contract.Id);
            var ContractLocations = new List<ContractLocation>();
            for (int i = 0; i < locs.Count; i++)
            {
                ContractLocation? contractLocation = new ContractLocation();
                contractLocation.ContractId = contractUpdate.Id;
                contractLocation.LocationId = locs[i];
                contractLocation.ExaminationCost = double.Parse(costs[i].Replace('.', ','));
                ContractLocations.Add(contractLocation);
            }
            contractUpdate.ContractLocations = ContractLocations;
            db.Contracts.Update(contractUpdate);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Contract? contract = await db.Contracts.FirstOrDefaultAsync(p => p.Id == id);
                if (contract != null)
                {
                    db.Contracts.Remove(contract);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
