using System.Collections.Generic;
using System.Threading.Tasks;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Application.Abstraction
{
    public interface IUserService
    {
        Task<IList<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
