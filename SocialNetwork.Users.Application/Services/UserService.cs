using AutoMapper;
using SocialNetwork.Users.Application.DTOs;
using SocialNetwork.Users.Application.Interfaces;
using SocialNetwork.Users.Domain.Entities;
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
}