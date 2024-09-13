using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Users.Application.Interfaces;
using SocialNetwork.Users.Application.Services;

namespace SocialNetwork.Users.CrossCutting.IoC.Modules;

public static class ApplicationModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
    }
}