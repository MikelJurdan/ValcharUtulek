using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Infrastructure.Database.Seeding
{
    public class AnimalSeeder
    {
        public List<Animal> GetAnimals()
        {
            return new List<Animal>
            {
                new Animal { Name = "Adolf", Species = "Pes", Gender = "Samec", Age = 5, IsAvailable = true, Photo = "adolf.jpg", Description = "Přátelský a hravý pes." },
                new Animal { Name = "Micka", Species = "Kočka", Gender = "Samice", Age = 2, IsAvailable = true, Photo = "micka.jpg", Description = "Klidná a mazlivá kočka." },
                new Animal { Name = "Azor", Species = "Pes", Gender = "Samec", Age = 3, IsAvailable = true, Photo = "azor.jpg", Description = "Potřebuje aktivního majitele." },
                new Animal { Name = "Líza", Species = "Kočka", Gender = "Samice", Age = 7, IsAvailable = false, Photo = "liza.jpg", Description = "Již našla domov." },
                new Animal { Name = "Arleta", Species = "Králík", Gender = "Samice", Age = 1, IsAvailable = true, Photo = "arleta.jpg", Description = "Mazlivá a společenská." }
            };
        }
    }
}
