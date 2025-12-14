using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Domain.Entities;
using ValcharUtulek.Infrastructure.Database;

namespace ValcharUtulek.Application.Implementation
{
    public class NewsService : INewsService
    {
        private readonly ShelterDbContext _db;

        public NewsService(ShelterDbContext db)
        {
            _db = db;
        }

        public async Task<IList<News>> GetAllNewsAsync()
        {
            return await _db.News.ToListAsync();
        }

        public async Task<News?> GetNewsByIdAsync(int id)
        {
            return await _db.News.FindAsync(id);
        }

        public async Task CreateNewsAsync(News news)
        {
            _db.News.Add(news);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateNewsAsync(News news)
        {
            _db.Entry(news).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteNewsAsync(int id)
        {
            var news = await _db.News.FindAsync(id);
            if (news != null)
            {
                _db.News.Remove(news);
                await _db.SaveChangesAsync();
            }
        }
    }
}
