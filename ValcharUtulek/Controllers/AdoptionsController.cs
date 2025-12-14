using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Controllers
{
    public class AdoptionsController : Controller
    {
        private readonly IAdoptionService _adoptionService;
        private readonly IAnimalService _animalService;

        public AdoptionsController(IAdoptionService adoptionService, IAnimalService animalService)
        {
            _adoptionService = adoptionService;
            _animalService = animalService;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adoptions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UzivatelId,ZvireId,DatumAdopce,Castka")] Adoption adoption)
        {
            if (ModelState.IsValid)
            {
                await _adoptionService.CreateAdoptionAsync(adoption);
                return RedirectToAction(nameof(Index));
            }
            return View(adoption);
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
