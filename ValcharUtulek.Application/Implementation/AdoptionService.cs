using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Domain.Entities;
using ValcharUtulek.Infrastructure.Database;

namespace ValcharUtulek.Application.Implementation
{
    public class AdoptionService : IAdoptionService
    {
        private readonly ShelterDbContext _db;

        public AdoptionService(ShelterDbContext db)
        {
            _db = db;
        }

        public async Task<IList<Adoption>> GetAllAdoptionsAsync()
        {
            return await _db.Adoptions.ToListAsync();
        }

        public async Task<Adoption?> GetAdoptionByIdAsync(int id)
        {
            return await _db.Adoptions.FindAsync(id);
        }

        public async Task CreateAdoptionAsync(Adoption adoption)
        {
            _db.Adoptions.Add(adoption);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAdoptionAsync(Adoption adoption)
        {
            _db.Entry(adoption).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAdoptionAsync(int id)
        {
            var adoption = await _db.Adoptions.FindAsync(id);
            if (adoption != null)
            {
                _db.Adoptions.Remove(adoption);
                await _db.SaveChangesAsync();
            }
        }
    }
}
