using ValcharUtulek.Domain.Entities;
using System.Collections.Generic;

namespace ValcharUtulek.Infrastructure.Database.Seeding
{
    public class GiftSeeder
    {
        public List<Gift> GetGifts()
        {
            return new List<Gift>
            {
                new Gift { UserId = 2, Amount = 250, GiftDate = new DateOnly(2023, 7, 1) },
                new Gift { UserId = 3, Amount = 1000, GiftDate = new DateOnly(2023, 7, 5) },
                new Gift { UserId = 4, Amount = 150, GiftDate = new DateOnly(2023, 7, 8) }
            };
        }
    }
}
