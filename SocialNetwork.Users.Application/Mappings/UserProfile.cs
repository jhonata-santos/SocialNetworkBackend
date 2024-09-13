using AutoMapper;
using SocialNetwork.Users.Application.DTOs;
using SocialNetwork.Users.Domain.Entities;

namespace SocialNetwork.Users.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, ListUsersDto>();
    }
}