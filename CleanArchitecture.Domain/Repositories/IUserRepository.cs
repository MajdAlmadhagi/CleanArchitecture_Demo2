using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Domain.Repositories;
//Repository is implemented in infrastructure layer 
public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}