using System.Threading.Tasks;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Application.Abstraction
{
    public interface IAccountService
    {
        Task<User?> RegisterAsync(string name, string email, string password);
        Task<User?> LoginAsync(string name, string password);
        Task LogoutAsync();
    }
}
