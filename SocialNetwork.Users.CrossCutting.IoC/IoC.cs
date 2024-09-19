using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Users.CrossCutting.IoC.Modules;

namespace SocialNetwork.Users.CrossCutting.IoC;

public static class IoC
{
    public static IServiceCollection ConfigureContainer(IServiceCollection services, IConfiguration configuration)
    {
        ApplicationModule.Register(services, configuration);
        InfrastructureModule.Register(services, configuration);
        SecurityModule.Register(services, configuration);
        return services;
    }
}