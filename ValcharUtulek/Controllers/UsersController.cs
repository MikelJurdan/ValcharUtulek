using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ValcharUtulek.Application.Abstraction;
using ValcharUtulek.Models;
using Microsoft.EntityFrameworkCore;
using ValcharUtulek.Infrastructure.Database;

namespace ValcharUtulek.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAdoptionService _adoptionService;
        private readonly IGiftService _giftService;
        private readonly IAnimalService _animalService;
        private readonly ShelterDbContext _context;

        public UsersController(IUserService userService, IAdoptionService adoptionService, IGiftService giftService, IAnimalService animalService, ShelterDbContext context)
        {
            _userService = userService;
            _adoptionService = adoptionService;
            _giftService = giftService;
            _animalService = animalService;
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var adoptions = await _adoptionService.GetAllAdoptionsAsync();
            var animals = await _animalService.GetAllAnimalsAsync();

            var userAdoptions = adoptions
                .Where(a => a.UserId == userId)
                .Join(animals,
                    adoption => adoption.AnimalId,
                    animal => animal.AnimalId,
                    (adoption, animal) => new AdoptionDetailViewModel
                    {
                        AdoptionId = adoption.AdoptionId,
                        AnimalId = animal.AnimalId,
                        AnimalName = animal.Name,
                        AnimalSpecies = animal.Species,
                        AdoptionDate = adoption.AdoptionDate,
                        Amount = adoption.Amount
                    })
                .ToList();

            var gifts = (await _giftService.GetAllGiftsAsync())
                .Where(g => g.UserId == userId)
                .OrderByDescending(g => g.GiftDate)
                .ToList();

            var viewModel = new UserDetailViewModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate,
                Role = user.Role,
                Adoptions = userAdoptions,
                Gifts = gifts
            };

            return View(viewModel);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var adoptions = await _context.Adoptions
                .Where(a => a.UserId == id)
                .Include(a => a.Animal)
                .ToListAsync();

            var adoptionsViewModel = adoptions
                .Select(a => new AdoptionDetailViewModel
                {
                    AdoptionId = a.AdoptionId,
                    AnimalId = a.AnimalId,
                    AdoptionDate = a.AdoptionDate,
                    Amount = a.Amount,
                    AnimalName = a.Animal?.Name ?? "Neznámé zvíře",
                    AnimalSpecies = a.Animal?.Species ?? "Neznámý druh"
                })
                .ToList();

            var gifts = await _context.Gifts
                .Where(g => g.UserId == id)
                .OrderByDescending(g => g.GiftDate)
                .ToListAsync();

            var viewModel = new UserDetailViewModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate,
                Role = user.Role,
                Adoptions = adoptionsViewModel,
                Gifts = gifts
            };

            return View("Index", viewModel);
        }
    }
}
