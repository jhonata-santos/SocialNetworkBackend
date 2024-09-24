using SocialNetwork.Users.Application.DTOs;

namespace SocialNetwork.Users.Application.Interfaces;

public interface IUserService
{
    Task<int> CreateUserAsync(CreateUserDto userDto);
    Task<UserDto> GeUserByIdAsync(int id);
    Task<IEnumerable<ListUsersDto>> GetAllUsersAsync();
    Task<bool> ChangeUserStatusAsync(UserStatusDto userDto);
    Task<bool> UpdatePasswordUser(UserPasswordDto userDto);
}