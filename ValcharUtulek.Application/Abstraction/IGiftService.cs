using System.Collections.Generic;
using System.Threading.Tasks;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Application.Abstraction
{
    public interface IGiftService
    {
        Task<IList<Gift>> GetAllGiftsAsync();
        Task<Gift?> GetGiftByIdAsync(int id);
        Task CreateGiftAsync(Gift gift);
        Task UpdateGiftAsync(Gift gift);
        Task DeleteGiftAsync(int id);
    }
}
