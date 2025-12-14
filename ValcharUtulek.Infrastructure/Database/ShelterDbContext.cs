using Microsoft.EntityFrameworkCore;
using ValcharUtulek.Domain.Entities;
using ValcharUtulek.Infrastructure.Database.Seeding;

namespace ValcharUtulek.Infrastructure.Database
{
    public class ShelterDbContext : DbContext
    {   
        public ShelterDbContext (DbContextOptions <ShelterDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Animal> Animals => Set<Animal>();
        public DbSet<Adoption> Adoptions => Set<Adoption>();
        public DbSet<Gift> Gifts => Set<Gift>();
        public DbSet<News> News => Set<News>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // Mapování sloupce password_hash v databázi na vlastnost PasswordHash
            mb.Entity<User>()
              .Property(u => u.PasswordHash)
              .HasColumnName("password_hash");

            // Konverze enum Role na string v databázi
            mb.Entity<User>()
              .Property(u => u.Role)
              .HasConversion<string>();
        }
    }
}

