using Microsoft.AspNetCore.Identity;
using ValcharUtulek.Domain.Entities;
using System.Collections.Generic;

namespace ValcharUtulek.Infrastructure.Database.Seeding
{
    public class UserSeeder
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserSeeder(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public List<User> GetUsers()
        {
            // Create temp user objects for hashing
            var adminUser = new User { Name = "admin" };
            var filipUser = new User { Name = "Filip" };
            var janaUser = new User { Name = "Jana" };
            var pavelUser = new User { Name = "Pavel" };

            var users = new List<User>
            {
                new User
                {
                    Name = "admin",
                    Email = "admin@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEFxOLi9abhb5kWb6EiUSS08uckMsN8Q0lHuwALAdFJL0PD2WJVcVuuE+uwE7jaOa3A==",
                    RegistrationDate = new DateOnly(2023, 1, 1),
                    Role = Role.Admin
                },
                new User
                {
                    Name = "Filip",
                    Email = "filip.novak@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEBAd9lzFiXvCnaZuYhvlHG/s7lGLDwwIasflLiqslvsZJrY+EWsiQAiwOV0c03BIkA==",
                    RegistrationDate = new DateOnly(2023, 1, 15),
                    Role = Role.Zakaznik
                },
                new User
                {
                    Name = "Jana",
                    Email = "jana.svobodova@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEOAJQYMjRT0wNWXzNZjN1bVU1htTnBHDn3MFOcOctG/UpWy8JVhpLrDBQGm+jViFFA==",
                    RegistrationDate = new DateOnly(2023, 2, 20),
                    Role = Role.Zakaznik
                },
                new User
                {
                    Name = "Pavel",
                    Email = "pavel.dvorak@example.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEP7VYGAetzNh2dgWUTsz13yWVuYG6JERBV76XGayGB3AIN6QW8CsNysTiJ1aXVqB5w==",
                    RegistrationDate = new DateOnly(2023, 3, 25),
                    Role = Role.Zakaznik
                }
            };

            return users;
        }
    }
}
