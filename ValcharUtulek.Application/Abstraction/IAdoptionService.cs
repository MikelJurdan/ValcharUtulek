using System.Collections.Generic;
using System.Threading.Tasks;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Application.Abstraction
{
    public interface IAdoptionService
    {
        Task<IList<Adoption>> GetAllAdoptionsAsync();
        Task<Adoption?> GetAdoptionByIdAsync(int id);
        Task CreateAdoptionAsync(Adoption adoption);
        Task UpdateAdoptionAsync(Adoption adoption);
        Task DeleteAdoptionAsync(int id);
    }
}
