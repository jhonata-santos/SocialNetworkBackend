using SocialNetwork.Users.Application.DTOs;

namespace SocialNetwork.Users.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> GeUserByIdAsync(int id);
    Task<IEnumerable<ListUsersDto>> GetAllUsersAsync();
}