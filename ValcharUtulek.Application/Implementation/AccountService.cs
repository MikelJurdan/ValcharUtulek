using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Domain.Entities;
using ValcharUtulek.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ValcharUtulek.Application.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly ShelterDbContext _db;
        private readonly IPasswordHasher<User> _hasher;
        private readonly ILogger<AccountService> _logger;

        public AccountService(ShelterDbContext db, IPasswordHasher<User> hasher,ILogger<AccountService> logger)
        {
            _db = db;
            _hasher = hasher;
            _logger = logger;
        }

        public async Task<User?> RegisterAsync(string name, string email, string password)
        {
            // Kontrola, zda uživatel s daným jménem již neexistuje
            if (await _db.Users.AnyAsync(u => u.Name == name))
            {
                return null;
            }

            // Vytvoření nového uživatele
            var user = new User
            {
                Name = name,
                Email = email,
                RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Role = Role.Zakaznik
            };
            // Hashování hesla
            user.PasswordHash = _hasher.HashPassword(user, password);

            // Přidání uživatele do databáze
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Uživatel {Name} se úspěšně zaregistroval.", user.Name);

            return user;
        }

        public async Task<User?> LoginAsync(string name, string password)
        {
            // Nalezení uživatele podle jména
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Name == name);
            if (user != null)
            {
                // Ověření hesla
                var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
                if (result == PasswordVerificationResult.Success)
                {
                    _logger.LogInformation("Uživatel {Name} se úspěšně přihlásil.", user.Name);
                    return user;
                }
            }
            return null;
        }

        public Task LogoutAsync()
        {
            _logger.LogInformation("Uživatel se odhlásil.");
            return Task.CompletedTask;
        }
    }
}
