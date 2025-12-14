using System.Collections.Generic;
using System.Threading.Tasks;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Application.Abstraction
{
    public interface IAnimalService
    {
        Task<IList<Animal>> GetAllAnimalsAsync();
        Task<Animal?> GetAnimalByIdAsync(int id);
        Task CreateAnimalAsync(Animal animal);
        Task UpdateAnimalAsync(Animal animal);
        Task DeleteAnimalAsync(int id);
    }
}
