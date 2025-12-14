using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Domain.Entities;
using ValcharUtulek.Infrastructure.Database;

namespace ValcharUtulek.Application.Implementation
{
    public class AnimalService : IAnimalService
    {
        private readonly ShelterDbContext _db;

        public AnimalService(ShelterDbContext db)
        {
            _db = db;
        }

        public async Task<IList<Animal>> GetAllAnimalsAsync()
        {
            return await _db.Animals.ToListAsync();
        }

        public async Task<Animal?> GetAnimalByIdAsync(int id)
        {
            return await _db.Animals.FindAsync(id);
        }

        public async Task CreateAnimalAsync(Animal animal)
        {
            _db.Animals.Add(animal);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAnimalAsync(Animal animal)
        {
            _db.Entry(animal).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAnimalAsync(int id)
        {
            var animal = await _db.Animals.FindAsync(id);
            if (animal != null)
            {
                _db.Animals.Remove(animal);
                await _db.SaveChangesAsync();
            }
        }
    }
}
