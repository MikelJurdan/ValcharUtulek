using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Domain.Entities;
using ValcharUtulek.Infrastructure.Database;

namespace ValcharUtulek.Application.Implementation
{
    public class GiftService : IGiftService
    {
        private readonly ShelterDbContext _db;

        public GiftService(ShelterDbContext db)
        {
            _db = db;
        }

        public async Task<IList<Gift>> GetAllGiftsAsync()
        {
            return await _db.Gifts.ToListAsync();
        }

        public async Task<Gift?> GetGiftByIdAsync(int id)
        {
            return await _db.Gifts.FindAsync(id);
        }

        public async Task CreateGiftAsync(Gift gift)
        {
            _db.Gifts.Add(gift);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateGiftAsync(Gift gift)
        {
            _db.Entry(gift).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteGiftAsync(int id)
        {
            var gift = await _db.Gifts.FindAsync(id);
            if (gift != null)
            {
                _db.Gifts.Remove(gift);
                await _db.SaveChangesAsync();
            }
        }
    }
}
