using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Controllers
{
    public class GiftsController : Controller
    {
        private readonly IGiftService _giftService;

        public GiftsController(IGiftService giftService)
        {
            _giftService = giftService;
        }

        // GET: Gifts
        public async Task<IActionResult> Index()
        {
            return View(await _giftService.GetAllGiftsAsync());
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

        private async Task<bool> GiftExists(int id)
        {
            return await _giftService.GetGiftByIdAsync(id) != null;
        }
    }
}
