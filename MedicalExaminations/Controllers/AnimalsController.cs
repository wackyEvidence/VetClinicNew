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
    }
}