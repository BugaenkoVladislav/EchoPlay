using Infrastructure.EchoPlay.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EchoPlay;

public static class InfrastructureExt
{
    public static void AddRepos(this IServiceCollection services)
    {
        services.AddScoped<UserRepository>();
        services.AddScoped<MessageRepository>();
    }
}