using ValcharUtulek.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ValcharUtulek.Infrastructure.Database.Seeding
{
    public class NewsSeeder
    {
        public List<News> GetNews()
        {
            return new List<News>
            {
                new News { Title = "Vítejte", Content = "První zpráva.", DateAdded = DateOnly.FromDateTime(DateTime.UtcNow), AuthorId = 1 }
            };
        }
    }
}
