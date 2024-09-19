using SocialNetwork.Users.Domain.Entities;

namespace SocialNetwork.Users.Domain.Interfaces;

public interface IUserRepository
{
    Task<int> CreateAsync(User user);
    Task<User> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
}