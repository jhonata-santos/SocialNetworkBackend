using AutoMapper;
using SocialNetwork.Users.Application.DTOs;
using SocialNetwork.Users.Application.Interfaces;
using SocialNetwork.Users.Domain.Entities;
using SocialNetwork.Users.Domain.Interfaces;

namespace SocialNetwork.Users.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
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
}