using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalExaminations.Models;
using ClosedXML.Excel;

namespace MedicalExaminations.Controllers
{
    public class LogEntriesController : Controller
    {
        private readonly AppDbContext db;

        public LogEntriesController(AppDbContext context)
        {
            db = context;
        }

        // GET: LogEntries
        public async Task<IActionResult> Index()
        {
            var appDbContext = db.LogEntries.Include(l => l.ActionType).Include(l => l.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: LogEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.LogEntries == null)
            {
                return NotFound();
            }

            var logEntry = await db.LogEntries
                .Include(l => l.ActionType)
                .Include(l => l.User)
                .Include(l => l.User.Workplace)
                .Include(l => l.User.Position)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (logEntry == null)
            {
                return NotFound();
            }
            ViewBag.ActionType = logEntry.ActionType.Display;
            //ViewBag.ActionTypes = new SelectList(db.ActionTypes.ToList(), "Id", "Name");
            return View(logEntry);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                LogEntry? logEntry = await db.LogEntries.FirstOrDefaultAsync(p => p.Id == id);
                if (logEntry != null)
                {
                    db.LogEntries.Remove(logEntry);
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
                var worksheet = workbook.Worksheets.Add("Logs");
                worksheet.Style.Font.FontSize = 14;
                worksheet.Cell("A1").Value = "Фамилия";
                worksheet.Cell("B1").Value = "Имя";
                worksheet.Cell("C1").Value = "Отчество";
                worksheet.Cell("D1").Value = "Телефон";
                worksheet.Cell("E1").Value = "Электронная почта";
                worksheet.Cell("F1").Value = "Организация";
                worksheet.Cell("G1").Value = "Наименование структурного подразделения";
                worksheet.Cell("H1").Value = "Должность";
                worksheet.Cell("I1").Value = "Рабочий телефон";
                worksheet.Cell("J1").Value = "Рабочий адрес электронной почты";
                worksheet.Cell("K1").Value = "Логин";
                worksheet.Cell("L1").Value = "Тип действия";
                worksheet.Cell("M1").Value = "Дата и время";
                worksheet.Cell("N1").Value = "Идентификационный номер экземпляра объекта";
                worksheet.Cell("O1").Value = "Описание экземпляра объекта после совершения действия";
                worksheet.Cell("P1").Value = "Идентификационный номер загруженного файла";
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheet.Range(1, 1, 1, 16).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 16).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 16).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 16).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                var list = db.LogEntries
                .Include(l => l.ActionType)
                .Include(l => l.User)
                .Include(l => l.User.Workplace)
                .Include(l => l.User.Position)
                .ToList();

                for (int i = 0; i < list.Count(); i++)
                {
                    worksheet.Cell(i + 2, 1).Value = list[i].User.Surname;
                    worksheet.Cell(i + 2, 2).Value = list[i].User.Name;
                    worksheet.Cell(i + 2, 3).Value = list[i].User.Patronymic;
                    worksheet.Cell(i + 2, 4).Value = list[i].User.PersonalPhone;
                    worksheet.Cell(i + 2, 5).Value = list[i].User.PersonalEmail;
                    worksheet.Cell(i + 2, 6).Value = list[i].User.Workplace.Name;
                    worksheet.Cell(i + 2, 7).Value = list[i].User.UnitName;
                    worksheet.Cell(i + 2, 8).Value = list[i].User.Position.Name;
                    worksheet.Cell(i + 2, 9).Value = list[i].User.WorkPhone;
                    worksheet.Cell(i + 2, 10).Value = list[i].User.WorkEmail;
                    worksheet.Cell(i + 2, 11).Value = list[i].User.Login;
                    worksheet.Cell(i + 2, 12).Value = list[i].ActionType.Display;
                    worksheet.Cell(i + 2, 13).Value = list[i].Timestamp;
                    worksheet.Cell(i + 2, 14).Value = list[i].ObjectId;
                    worksheet.Cell(i + 2, 15).Value = list[i].ObjectAttributes;
                    worksheet.Cell(i + 2, 16).Value = list[i].FileIdDisplay;

                    worksheet.Row(i + 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(i + 2, 1, i + 2, 16).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(i + 2, 1, i + 2, 16).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(i + 2, 1, i + 2, 16).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(i + 2, 1, i + 2, 16).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                }

                using (var stream = new MemoryStream())
                {
                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "logs.xlsx"
                    };
                }

            }
        }
    }
}
