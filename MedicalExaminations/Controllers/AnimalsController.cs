using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using MedicalExaminations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MedicalExaminations.Controllers

{
    public class AnimalsController : Controller
    {
        AppDbContext db;

        public AnimalsController(AppDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CanEditAnimalsRegistry = GlobalConfig.CurrentUser.PermissionManager.CanEditAnimalsRegistry;
            return View(await db.Animals
                .Include(a => a.Location)
                .Include(a => a.AnimalCategory)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Locations = new SelectList(db.Locations.ToList(), "Id", "Name");
            ViewBag.AnimalCategories = new SelectList(db.AnimalCategories.ToList(), "Id", "Name");
            ViewBag.OwnerSigns = db.OwnerSigns.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Animal animal, IFormFileCollection animalPhotos, IEnumerable<int> ownerSigns)
        {
            db.Animals.Add(animal);
            if (animalPhotos != null)
            {
                var AnimalPhotosList = new List<AnimalPhoto>();
                foreach (var photo in animalPhotos)
                {
                    if (photo != null)
                    {
                        using (var br = new BinaryReader(photo.OpenReadStream()))
                        {
                            var data = br.ReadBytes((int)photo.Length);
                            var img = new AnimalPhoto { AnimalId = animal.Id };
                            img.PhotoData = data;
                            AnimalPhotosList.Add(img);
                        }
                    }
                }
                animal.AnimalPhotos = AnimalPhotosList;
            }
            if (ownerSigns != null)
            {
                var OwnerSignsList = new List<OwnerSign>();
                foreach (var ownerSign in ownerSigns)
                {
                    if (ownerSign != null)
                    {
                        OwnerSign? OwnerSign = await db.OwnerSigns.FirstOrDefaultAsync(p => p.Id == ownerSign);
                        OwnerSignsList.Add(OwnerSign);
                    }
                }
                animal.OwnerSigns = OwnerSignsList;
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Locations = new SelectList(db.Locations.ToList(), "Id", "Name");
            ViewBag.AnimalCategories = new SelectList(db.AnimalCategories.ToList(), "Id", "Name");
            ViewBag.OwnerSigns = db.OwnerSigns.ToList();
            if (id != null)
            {
                Animal? animal = await db.Animals.Include(a => a.AnimalPhotos).Include(a => a.OwnerSigns).Include(a => a.MedicalExaminations).FirstOrDefaultAsync(p => p.Id == id);
                if (animal != null) return View(animal);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Animal animal, IFormFileCollection animalPhotos, IEnumerable<int> ownerSigns)
        {
            db.Animals.Update(animal);
            Animal? animalUpdate = await db.Animals.Include(a => a.AnimalPhotos).Include(a => a.OwnerSigns).Include(a => a.MedicalExaminations).FirstOrDefaultAsync(p => p.Id == animal.Id);
            if (animalPhotos.Count != 0)
            {
                var AnimalPhotosList = new List<AnimalPhoto>();
                foreach (var photo in animalPhotos)
                {
                    if (photo != null)
                    {
                        using (var br = new BinaryReader(photo.OpenReadStream()))
                        {
                            var data = br.ReadBytes((int)photo.Length);
                            var img = new AnimalPhoto { AnimalId = animal.Id };
                            img.PhotoData = data;
                            AnimalPhotosList.Add(img);
                        }
                    }
                }
                animalUpdate.AnimalPhotos = AnimalPhotosList;
            }
            var OwnerSignsList = new List<OwnerSign>();
            foreach (var ownerSign in ownerSigns)
            {
                if (ownerSign != null)
                {
                    OwnerSign? OwnerSign = await db.OwnerSigns.FirstOrDefaultAsync(p => p.Id == ownerSign);
                    OwnerSignsList.Add(OwnerSign);
                }
            }
            animalUpdate.OwnerSigns = OwnerSignsList;
            db.Animals.Update(animalUpdate);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Animal? animal = await db.Animals.FirstOrDefaultAsync(p => p.Id == id);
                if (animal != null)
                {
                    db.Animals.Remove(animal);
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
                var worksheet = workbook.Worksheets.Add("Animals");
                worksheet.Style.Font.FontSize = 14;
                worksheet.Cell("A1").Value = "Регистрационный номер";
                worksheet.Cell("B1").Value = "Населённый пункт";
                worksheet.Cell("C1").Value = "Категория животного";
                worksheet.Cell("D1").Value = "Пол животного";
                worksheet.Cell("E1").Value = "Год рождения";
                worksheet.Cell("F1").Value = "Кличка животного";
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheet.Range(1, 1, 1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 6).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 6).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                worksheet.Range(1, 1, 1, 6).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                var list = db.Animals.OrderBy(x => x.Id)
                .Include(a => a.Location)
                .Include(a => a.AnimalCategory)
                .ToList();

                for (int i = 0; i < list.Count(); i++)
                {
                    worksheet.Cell(i + 2, 1).Value = list[i].RegistrationNumber;
                    worksheet.Cell(i + 2, 2).Value = list[i].Location.Name;
                    worksheet.Cell(i + 2, 3).Value = list[i].AnimalCategory.Name;
                    worksheet.Cell(i + 2, 4).Value = list[i].Sex;
                    worksheet.Cell(i + 2, 5).Value = list[i].BirthYear;
                    worksheet.Cell(i + 2, 6).Value = list[i].Nickname;
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
                        FileDownloadName = "animals.xlsx"
                    };
                }

            }
        }
    }
}