using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Users.Data.Repositories;
using SocialNetwork.Users.Domain.Interfaces;

namespace SocialNetwork.Users.CrossCutting.IoC.Modules;

public static class InfrastructureModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}