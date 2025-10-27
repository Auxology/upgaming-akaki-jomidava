using Microsoft.Extensions.DependencyInjection;
using Upgaming.Application.Authors.Quries;
using Upgaming.Application.Books.Commands;
using Upgaming.Application.Books.Queries;

namespace Upgaming.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register Book Commands
        services.AddScoped<AddBookCommand>();

        // Register Book Queries
        services.AddScoped<GetAllBooksQuery>();
        services.AddScoped<GetBooksByAuthorQuery>();

        // Register Author Queries
        services.AddScoped<GetAuthorDetailsQuery>();

        return services;
    }
}

