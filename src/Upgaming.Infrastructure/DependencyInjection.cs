using Microsoft.Extensions.DependencyInjection;
using Upgaming.Domain.Repositories;
using Upgaming.Infrastructure.Repositories;

namespace Upgaming.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register repositories as singletons since they use static in-memory storage
        services.AddSingleton<IAuthorRepository, InMemoryAuthorRepository>();
        services.AddSingleton<IBookRepository, InMemoryBookRepository>();

        return services;
    }
}