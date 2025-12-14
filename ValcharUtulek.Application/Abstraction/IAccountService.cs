using System.Threading.Tasks;
using ValcharUtulek.Application.ViewModels;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Application.Abstraction
{
    public interface IAccountService
    {
        Task<User?> RegisterAsync(RegisterViewModel model);
        Task<User?> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
