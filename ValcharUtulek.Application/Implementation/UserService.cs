using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Domain.Entities;
using ValcharUtulek.Infrastructure.Database;

namespace ValcharUtulek.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly ShelterDbContext _db;

        public UserService(ShelterDbContext db)
        {
            _db = db;
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task CreateUserAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }
    }
}
