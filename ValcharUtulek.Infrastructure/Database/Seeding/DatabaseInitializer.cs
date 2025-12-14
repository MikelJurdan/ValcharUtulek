using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Infrastructure.Database.Seeding
{
    public class DatabaseInitializer
    {
        private readonly ShelterDbContext _context;
        private readonly ILogger<DatabaseInitializer> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public DatabaseInitializer(ShelterDbContext context, ILogger<DatabaseInitializer> logger, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Aplikování všech neaplikovaných migrací na databázi
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Database migrations applied successfully.");

                // Postupné seedování jednotlivých tabulek daty
                await SeedAdminAsync();
                await SeedUsersAsync();
                await SeedAnimalsAsync();
                await SeedNewsAsync();
                await SeedAdoptionsAsync();
                await SeedGiftsAsync();

                _logger.LogInformation("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                // Logování a předání výjimky při chybě
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        private async Task SeedAdminAsync()
        {
            if (!_context.Users.Any(u => u.Role == Role.Admin))
            {
                var adminUser = new User
                {
                    Name = _configuration["AdminAccount:Username"] ?? "admin",
                    Email = _configuration["AdminAccount:Email"] ?? "admin@example.com",
                    RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    Role = Role.Admin
                };

                adminUser.PasswordHash = _passwordHasher.HashPassword(adminUser, "admin1");

                _context.Users.Add(adminUser);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Admin user created successfully with a dynamically generated hash.");
            }
        }

        // Přidá do databáze uživatele ze seederu, pokud ještě neexistují (podle e-mailu)
        private async Task SeedUsersAsync()
        {
            var users = UserSeeder.GetUsers();
            int added = 0;
            int updated = 0;

            foreach (var user in users)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (existingUser == null)
                {
                    await _context.Users.AddAsync(user);
                    added++;
                }
                else
                {
                    try
                    {
                        if (string.IsNullOrEmpty(existingUser.PasswordHash) || 
                            !IsValidBase64(existingUser.PasswordHash))
                        {
                            existingUser.PasswordHash = user.PasswordHash;
                            _context.Users.Update(existingUser);
                            updated++;
                        }
                    }
                    catch
                    {
                        // Pokud dojde k chybě při kontrole, aktualizuje se hash
                        existingUser.PasswordHash = user.PasswordHash;
                        _context.Users.Update(existingUser);
                        updated++;
                    }
                }
            }

            if (added > 0 || updated > 0)
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Seeded {AddedCount} new users and updated {UpdatedCount} users with corrected password hashes.", added, updated);
            }
            else
            {
                _logger.LogInformation("All users from seeder already exist with valid hashes, skipping user seeding.");
            }
        }

        private bool IsValidBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Přidá do databáze zvířata ze seederu, pokud je tabulka prázdná
        private async Task SeedAnimalsAsync()
        {
            if (!await _context.Animals.AnyAsync())
            {
                var animalSeeder = new AnimalSeeder();
                var animals = animalSeeder.GetAnimals();

                await _context.Animals.AddRangeAsync(animals);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeded {Count} animals.", animals.Count);
            }
            else
            {
                _logger.LogInformation("Animals already exist, skipping animal seeding.");
            }
        }

        // Přidá do databáze novinky ze seederu, pokud je tabulka prázdná
        private async Task SeedNewsAsync()
        {
            var newsItems = NewsSeeder.GetNews();
            int addedCount = 0;

            foreach (var newsItem in newsItems)
            {
                if (!await _context.News.AnyAsync(n => n.Title == newsItem.Title))
                {
                    await _context.News.AddAsync(newsItem);
                    addedCount++;
                }
            }

            if (addedCount > 0)
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Seeded {Count} new news items.", addedCount);
            }
            else
            {
                _logger.LogInformation("All news items from seeder already exist, skipping news seeding.");
            }
        }

        // Přidá do databáze adopce ze seederu, pokud je tabulka prázdná
        private async Task SeedAdoptionsAsync()
        {
            if (!await _context.Adoptions.AnyAsync())
            {
                var adoptionSeeder = new AdoptionSeeder();
                var adoptions = adoptionSeeder.GetAdoptions();

                await _context.Adoptions.AddRangeAsync(adoptions);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeded {Count} adoptions.", adoptions.Count);
            }
            else
            {
                _logger.LogInformation("Adoptions already exist, skipping adoption seeding.");
            }
        }

        // Přidá do databáze dary ze seederu, pokud je tabulka prázdná
        private async Task SeedGiftsAsync()
        {
            if (!await _context.Gifts.AnyAsync())
            {
                var giftSeeder = new GiftSeeder();
                var gifts = giftSeeder.GetGifts();

                await _context.Gifts.AddRangeAsync(gifts);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeded {Count} gifts.", gifts.Count);
            }
            else
            {
                _logger.LogInformation("Gifts already exist, skipping gift seeding.");
            }
        }
    }
}
