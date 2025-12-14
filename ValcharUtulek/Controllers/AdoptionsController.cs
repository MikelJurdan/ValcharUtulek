using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Domain.Entities;
using ValcharUtulek.Infrastructure.Database;
using ValcharUtulek.Models;

namespace ValcharUtulek.Controllers
{
    public class AdoptionsController : Controller
    {
        private readonly IAdoptionService _adoptionService;
        private readonly IAnimalService _animalService;
        private readonly ShelterDbContext _db;

        public AdoptionsController(IAdoptionService adoptionService, IAnimalService animalService, ShelterDbContext db)
        {
            _adoptionService = adoptionService;
            _animalService = animalService;
            _db = db;
        }

        // GET: Adoptions
        public async Task<IActionResult> Index()
        {
            var animals = (await _animalService.GetAllAnimalsAsync()).Where(a => a.IsAvailable).ToList();
            return View(animals);
        }

        // GET: Adoptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _adoptionService.GetAdoptionByIdAsync(id.Value);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // GET: Adoptions/Create
        public async Task<IActionResult> Create()
        {
            var animals = await _animalService.GetAllAnimalsAsync();
            ViewBag.Animals = new SelectList(animals, "AnimalId", "Name");
            return View();
        }

        // POST: Adoptions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdoptionCreateViewModel model)
        {
            var animals = await _animalService.GetAllAnimalsAsync();
            ViewBag.Animals = new SelectList(animals, "AnimalId", "Name");

            if (ModelState.IsValid)
            {
                int animalId;
                if (!string.IsNullOrWhiteSpace(model.AnimalName) && !string.IsNullOrWhiteSpace(model.AnimalSpecies))
                {
                    // Vytvoření nového zvířete
                    var animal = new Animal
                    {
                        Name = model.AnimalName,
                        Species = model.AnimalSpecies,
                        Age = model.AnimalAge ?? 0,
                        Photo = model.AnimalPhoto,
                        IsAvailable = true
                    };
                    _db.Animals.Add(animal);
                    await _db.SaveChangesAsync();
                    animalId = animal.AnimalId;
                }
                else if (model.AnimalId.HasValue)
                {
                    animalId = model.AnimalId.Value;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Musíte vybrat nebo zadat zvíře.");
                    return View(model);
                }

                var adoption = new Adoption
                {
                    UserId = model.UserId,
                    AnimalId = animalId,
                    AdoptionDate = DateOnly.FromDateTime(model.AdoptionDate),
                };
                await _adoptionService.CreateAdoptionAsync(adoption);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // POST: Adoptions/Adopt
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adopt(int animalId, double amount)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var adoption = new Adoption
            {
                UserId = int.Parse(userId),
                AnimalId = animalId,
                AdoptionDate = DateOnly.FromDateTime(DateTime.Now),
                Amount = amount
            };

            await _adoptionService.CreateAdoptionAsync(adoption);

            var animal = await _db.Animals.FindAsync(animalId);
            if (animal != null)
            {
                animal.IsAvailable = false;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Users", new { id = userId });
        }

        // GET: Adoptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _adoptionService.GetAdoptionByIdAsync(id.Value);
            if (adoption == null)
            {
                return NotFound();
            }
            return View(adoption);
        }

        // POST: Adoptions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UzivatelId,ZvireId,DatumAdopce,Castka")] Adoption adoption)
        {
            if (id != adoption.AdoptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _adoptionService.UpdateAdoptionAsync(adoption);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AdoptionExists(adoption.AdoptionId))
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
            return View(adoption);
        }

        // GET: Adoptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _adoptionService.GetAdoptionByIdAsync(id.Value);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // POST: Adoptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _adoptionService.DeleteAdoptionAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AdoptionExists(int id)
        {
            return await _adoptionService.GetAdoptionByIdAsync(id) != null;
        }
    }
}
