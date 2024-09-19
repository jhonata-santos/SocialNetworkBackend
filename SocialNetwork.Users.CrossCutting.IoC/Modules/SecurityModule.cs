using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Users.Application.Interfaces;
using SocialNetwork.Users.CrossCutting.IoC.Services;

namespace SocialNetwork.Users.CrossCutting.IoC.Modules;

public static class SecurityModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }
}