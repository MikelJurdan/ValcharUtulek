using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Domain.Entities;
using ValcharUtulek.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ValcharUtulek.Controllers
{
    public class GiftsController : Controller
    {
        private readonly IGiftService _giftService;
        private readonly IAnimalService _animalService;

        public GiftsController(IGiftService giftService, IAnimalService animalService)
        {
            _giftService = giftService;
            _animalService = animalService;
        }

        // GET: Gifts
        public async Task<IActionResult> Index()
        {
            var animals = await _animalService.GetAllAnimalsAsync();
            var model = new GiftViewModel
            {
                Animals = animals,
                Gifts = await _giftService.GetAllGiftsAsync()
            };
            ViewBag.Animals = new SelectList(animals, "AnimalId", "Name");
            return View(model);
        }

        // GET: Gifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _giftService.GetGiftByIdAsync(id.Value);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // GET: Gifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GiftId,UserId,Amount,GiftDate")] Gift gift)
        {
            if (ModelState.IsValid)
            {
                await _giftService.CreateGiftAsync(gift);
                return RedirectToAction(nameof(Index));
            }
            return View(gift);
        }

        // GET: Gifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _giftService.GetGiftByIdAsync(id.Value);
            if (gift == null)
            {
                return NotFound();
            }
            return View(gift);
        }

        // POST: Gifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GiftId,UserId,Amount,GiftDate")] Gift gift)
        {
            if (id != gift.GiftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _giftService.UpdateGiftAsync(gift);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GiftExists(gift.GiftId))
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
            return View(gift);
        }

        // GET: Gifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _giftService.GetGiftByIdAsync(id.Value);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // POST: Gifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _giftService.DeleteGiftAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Gifts/Donate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Donate([Bind("AnimalId,Amount")] GiftViewModel model)
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    ModelState.AddModelError(string.Empty, "Nelze určit uživatele.");
                }
                else
                {
                    var gift = new Gift
                    {
                        UserId = int.Parse(userIdClaim.Value),
                        Amount = (double)model.Amount,
                        GiftDate = DateOnly.FromDateTime(DateTime.Now)
                        // případně zde přidejte AnimalId do entity Gift, pokud existuje
                    };
                    await _giftService.CreateGiftAsync(gift);
                    return RedirectToAction(nameof(Index));
                }
            }
            // Pokud je chyba, znovu načti data do modelu a ViewBag
            var animals = await _animalService.GetAllAnimalsAsync();
            model.Animals = animals;
            model.Gifts = await _giftService.GetAllGiftsAsync();
            ViewBag.Animals = new SelectList(animals, "AnimalId", "Name");
            return View("Index", model);
        }

        private async Task<bool> GiftExists(int id)
        {
            return await _giftService.GetGiftByIdAsync(id) != null;
        }
    }
}
