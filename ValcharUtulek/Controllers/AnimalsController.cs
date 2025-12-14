using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValcharUtulek.Domain.Entities;
using ValcharUtulek.Infrastructure.Database;

namespace ValcharUtulek.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AnimalsController : Controller
    {
        private readonly ShelterDbContext _db;

        public AnimalsController(ShelterDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _db.Animals.FirstOrDefaultAsync(a => a.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _db.Animals.FindAsync(id);
            if (animal != null)
            {
                _db.Animals.Remove(animal);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Adoptions");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _db.Animals.FirstOrDefaultAsync(a => a.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return View(animal);
            }
            _db.Entry(animal).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Adoptions");
        }
    }
}
