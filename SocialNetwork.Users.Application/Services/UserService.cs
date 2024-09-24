using AutoMapper;
using SocialNetwork.Users.Application.DTOs;
using SocialNetwork.Users.Application.Interfaces;
using SocialNetwork.Users.Domain.Entities;
using SocialNetwork.Users.Domain.Enums;
using SocialNetwork.Users.Domain.Interfaces;

namespace SocialNetwork.Users.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IMapper mapper, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> GeUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }

    public async Task<IEnumerable<ListUsersDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        var userDto = _mapper.Map<IEnumerable<ListUsersDto>>(users);
        return userDto;
    }

    public async Task<int> CreateUserAsync(CreateUserDto userDto)
    {
        userDto.Password = _passwordHasher.HashPassword(userDto.Password);
        var user = _mapper.Map<User>(userDto);
        var result = await _userRepository.CreateAsync(user);
        return result;
    }

    public async Task<bool> ChangeUserStatusAsync(UserStatusDto userDto)
    {
        User user = await _userRepository.GetUserByIdAsync(userDto.Id);
        if (user == null)
            throw new ArgumentException("User not found.");

        if (user.Available == (int)UserStatus.Disable && userDto.Available == (int)UserStatus.Disable)
            throw new InvalidOperationException("User is already deactivated.");
        else if (user.Available == (int)UserStatus.Active && userDto.Available == (int)UserStatus.Active)
            throw new InvalidOperationException("User is already active.");

        user.Available = userDto.Available == (int)UserStatus.Active ? (int)UserStatus.Active : (int)UserStatus.Disable;
        user.UpdateAt = DateTime.Now;

        var result = await _userRepository.UpdateAsync(user);
        if (!result)
            return false;

        return true;
    }

    public async Task<bool> UpdatePasswordUser(UserPasswordDto userDto)
    {
        User user = await _userRepository.GetUserByIdAsync(userDto.Id);
        if (user == null)
            throw new ArgumentException("User not found.");

        if (userDto.NewPassword == userDto.OldPassword)
            throw new InvalidOperationException("The new password must be different from the previous password.");

        if (_passwordHasher.VerifyPassword(userDto.OldPassword, user.Password))
        {
            user.Password = _passwordHasher.HashPassword(userDto.NewPassword);
            var result = await _userRepository.UpdateAsync(user);
            if (!result)
                return false;

            return true;
        }
        else
        {
            throw new InvalidOperationException("Invalid Password.");
        }
    }
}