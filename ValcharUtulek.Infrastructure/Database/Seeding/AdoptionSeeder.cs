using ValcharUtulek.Domain.Entities;
using System.Collections.Generic;

namespace ValcharUtulek.Infrastructure.Database.Seeding
{
    public class AdoptionSeeder
    {
        public List<Adoption> GetAdoptions()
        {
            return new List<Adoption>
            {
                new Adoption { UserId = 2, AnimalId = 4, AdoptionDate = new DateOnly(2023, 5, 10), Amount = 500 },
                new Adoption { UserId = 3, AnimalId = 1, AdoptionDate = new DateOnly(2023, 6, 15), Amount = 300 }
            };
        }
    }
}
