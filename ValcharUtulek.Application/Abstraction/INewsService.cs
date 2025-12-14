using System.Collections.Generic;
using System.Threading.Tasks;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Application.Abstraction
{
    public interface INewsService
    {
        Task<IList<News>> GetAllNewsAsync();
        Task<News?> GetNewsByIdAsync(int id);
        Task CreateNewsAsync(News news);
        Task UpdateNewsAsync(News news);
        Task DeleteNewsAsync(int id);
    }
}
