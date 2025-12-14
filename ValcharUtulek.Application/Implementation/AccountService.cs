using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Application.ViewModels;
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

        public AccountService(ShelterDbContext db, IPasswordHasher<User> hasher, ILogger<AccountService> logger)
        {
            _db = db;
            _hasher = hasher;
            _logger = logger;
        }

        public async Task<User?> RegisterAsync(RegisterViewModel model)
        {
            if (await _db.Users.AnyAsync(u => u.Name == model.Name))
            {
                return null;
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Role = Role.Zakaznik
            };
            user.PasswordHash = _hasher.HashPassword(user, model.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Uživatel {Name} se úspěšně zaregistroval.", user.Name);

            return user;
        }

        public async Task<User?> LoginAsync(LoginViewModel model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Name == model.Name);
            if (user != null)
            {
                var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
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
            _logger.LogInformation("User logged out.");
            return Task.CompletedTask;
        }
    }
}
