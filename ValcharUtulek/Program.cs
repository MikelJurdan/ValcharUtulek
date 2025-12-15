using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ValcharUtulek.Infrastructure.Database;
using ValcharUtulek.Infrastructure.Database.Seeding;
using ValcharUtulek.Domain.Entities;
using Serilog;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Application.Implementation;

namespace ValcharUtulek
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Konfigurace loggeru 
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/valcharutulek.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Spouštění webové aplikace");

                var builder = WebApplication.CreateBuilder(args);
                builder.Host.UseSerilog();

                // Získání připojovacího řetězce z konfigurace
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                // Konfigurace databázového kontextu
                builder.Services.AddDbContext<ShelterDbContext>(options =>
                {
                    var serverVersion = ServerVersion.AutoDetect(connectionString);
                    options.UseMySql(connectionString, serverVersion);
                });

                builder.Services.AddControllersWithViews();
                
                // Registrace IPasswordHasher<User> pro hashování hesel
                builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
                
                // Konfigurace PasswordHasher - nastavení kompatibility
                builder.Services.Configure<PasswordHasherOptions>(options =>
                {
                    options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
                });

                // Konfigurace cookie autentizace
                builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/Login";
                        options.LogoutPath = "/Account/Logout";
                        options.AccessDeniedPath = "/Account/AccessDenied";
                    });

                // Konfigurace autorizace
                builder.Services.AddAuthorization(options =>
                {
                    options.AddPolicy("AdminOnly", policy => policy.RequireRole(Role.Admin.ToString()));
                });

                // Registrace služeb aplikační logiky
                builder.Services.AddScoped<IAccountService, AccountService>();
                builder.Services.AddScoped<IAdoptionService, AdoptionService>();
                builder.Services.AddScoped<IAnimalService, AnimalService>();
                builder.Services.AddScoped<IGiftService, GiftService>();
                builder.Services.AddScoped<INewsService, NewsService>();
                builder.Services.AddScoped<IUserService, UserService>();

                var app = builder.Build();

                // Inicializace databáze s výchozími hodnotami
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<ShelterDbContext>();
                        var logger = services.GetRequiredService<ILogger<DatabaseInitializer>>();
                        var hasher = services.GetRequiredService<IPasswordHasher<User>>();
                        var configuration = services.GetRequiredService<IConfiguration>();

                        // Inicializace databáze pomocí DatabaseInitializer
                        var dbInitializer = new DatabaseInitializer(context, logger, hasher, configuration);
                        await dbInitializer.InitializeAsync();
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "Došlo k chybě při inicializaci databáze.");
                        throw;
                    }
                }

                // Konfigurace HTTP pipeline
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Index");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();

                // Mapování výchozí trasy pro kontrolery
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Aplikace byla neočekávaně ukončena");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}

