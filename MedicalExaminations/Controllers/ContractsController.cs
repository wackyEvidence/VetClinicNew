using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using MedicalExaminations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalExaminations.Logging;

namespace MedicalExaminations.Controllers
{
    public class ContractsController : Controller
    {
        AppDbContext db;
        private readonly ActionType createAction;
        private readonly ActionType updateAction;
        private readonly ActionType deleteAction;

        public ContractsController(AppDbContext context)
        {
            db = context;
            createAction = db.ActionTypes.Where(at => at.Name == "Create").First();
            updateAction = db.ActionTypes.Where(at => at.Name == "Update").First();
            deleteAction = db.ActionTypes.Where(at => at.Name == "Delete").First();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CanEditContractsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanEditOrganizationsRegistry;
            ViewBag.CanViewAnimalsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanViewAnimalsRegistry;
            ViewBag.CanViewOrganizationsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanViewOrganizationsRegistry;

            return View(db.Contracts
                .Include(a => a.Client)
                .Include(a => a.Executor)
                .Where(GlobalConfig.CurrentUser.PermissionManager.ContractsFilter)
                .ToList());
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
            Logger.GetInstance().Log(db, createAction, contract.Id, contract.ToJson());
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
            Logger.GetInstance().Log(db, updateAction, contract.Id, contract.ToJson());
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
                    Logger.GetInstance().Log(db, deleteAction, contract.Id);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public IActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Contracts");
                worksheet.Style.Font.FontSize = 14;
                worksheet.Cell("A1").Value = "Номер";
                worksheet.Cell("B1").Value = "Дата заключения";
                worksheet.Cell("C1").Value = "Дата действия";
                worksheet.Cell("D1").Value = "Исполнитель";
                worksheet.Cell("E1").Value = "Заказчик";
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheet.Range(1, 1, 1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 5).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 5).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 5).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                var list = db.Contracts.OrderByDescending(x=>x.SigningDate)
                .Include(a => a.Client)
                .Include(a => a.Executor)
                .ToList();


                for (int i = 0; i < list.Count(); i++)
                {
                    worksheet.Cell(i + 2, 1).Value = list[i].Number;
                    worksheet.Cell(i + 2, 2).Value = list[i].SigningDate.ToString();
                    worksheet.Cell(i + 2, 3).Value = list[i].ValidUntil.ToString();
                    worksheet.Cell(i + 2, 4).Value = list[i].Executor.Name;
                    worksheet.Cell(i + 2, 5).Value = list[i].Client.Name;
                    worksheet.Row(i + 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(i + 2, 1, i + 2, 5).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(i + 2, 1, i + 2, 5).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(i + 2, 1, i + 2, 5).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(i + 2, 1, i + 2, 5).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                }

                using (var stream = new MemoryStream())
                {
                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "contracts.xlsx"
                    };
                }

            }
        }
    }
}
