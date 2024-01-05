using MedicalExaminations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ClosedXML.Excel;

namespace MedicalExaminations.Controllers
{
    public class OrganizationsController : Controller
    {
        AppDbContext db;
        public OrganizationsController(AppDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.CanEditOrganizationsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanEditOrganizationsRegistry;
            return View(db.Organizations
                .Include(o => o.OrganizationType)
                .Include(o => o.OrganizationAttribute)
                .Include(o => o.Location)
                .Where(GlobalConfig.CurrentUser.PermissionManager.OrganizationsFilter)
        .ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Locations = new SelectList(db.Locations.ToList(), "Id", "Name");
            ViewBag.OrganizationTypes = new SelectList(db.OrganizationTypes.ToList(), "Id", "Name");
            ViewBag.OrganizationAttributes = new SelectList(db.OrganizationAttributes.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Organization organization)
        {
            db.Organizations.Add(organization);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Locations = new SelectList(db.Locations.ToList(), "Id", "Name");
            ViewBag.OrganizationTypes = new SelectList(db.OrganizationTypes.ToList(), "Id", "Name");
            ViewBag.OrganizationAttributes = new SelectList(db.OrganizationAttributes.ToList(), "Id", "Name");
            ViewBag.CanEditOrganizationsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanEditOrganizationsRegistry;
            if (id != null)
            {
                Organization? organization = await db.Organizations.FirstOrDefaultAsync(p => p.Id == id);
                if (organization != null) return View(organization);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Organization organization)
        {
            db.Organizations.Update(organization);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Organization? organization = await db.Organizations.FirstOrDefaultAsync(p => p.Id == id);
                if (organization != null)
                {
                    db.Organizations.Remove(organization);
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
                var worksheet = workbook.Worksheets.Add("Organizations");
                worksheet.Style.Font.FontSize = 14;
                worksheet.Cell("A1").Value = "Наименование";
                worksheet.Cell("B1").Value = "ИНН";
                worksheet.Cell("C1").Value = "КПП";
                worksheet.Cell("D1").Value = "Адрес регистрации";
                worksheet.Cell("E1").Value = "Тип организации";
                worksheet.Cell("F1").Value = "ИП/Юридическое лицо";
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheet.Range(1, 1, 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 6).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 6).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 6).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                var list = db.Organizations
                .Include(o => o.OrganizationType)
                .Include(o => o.OrganizationAttribute)
                .Include(o => o.Location)
                .ToList();

                for (int i = 0; i < list.Count(); i++)
                {
                    worksheet.Cell(i + 2, 1).Value = list[i].Name;
                    worksheet.Cell(i + 2, 2).Value = list[i].INN;
                    worksheet.Cell(i + 2, 3).Value = list[i].KPP;
                    worksheet.Cell(i + 2, 4).Value = $"{list[i].Location.Name}, ул.{list[i].Street}, д.{list[i].HouseNumber}";
                    worksheet.Cell(i + 2, 5).Value = list[i].OrganizationType.Name;
                    worksheet.Cell(i + 2, 6).Value = list[i].OrganizationAttribute.Name;
                    worksheet.Row(i + 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(i + 2, 1, i + 2, 6).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(i + 2, 1, i + 2, 6).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(i + 2, 1, i + 2, 6).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(i + 2, 1, i + 2, 6).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                }

                using (var stream = new MemoryStream())
                {
                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "organizations.xlsx"
                    };
                }

            }
        }
    }
}
