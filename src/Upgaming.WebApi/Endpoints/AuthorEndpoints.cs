using Upgaming.Application.DTOs;
using Upgaming.WebApi.Handlers.Authors;
using Upgaming.WebApi.Handlers.Books;

namespace Upgaming.WebApi.Endpoints;

/// <summary>
/// Configures all author-related API endpoints and maps them to their respective handlers.
/// </summary>
internal static class AuthorEndpoints
{
    public static IEndpointRouteBuilder MapAuthorEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/authors/{id}/books", GetBooksByAuthorHandler.HandleAsync)
            .WithTags(Tags.Authors)
            .WithName("GetBooksByAuthor")
            .Produces<IReadOnlyList<BookDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);

        app.MapGet("/api/authors/{id}", GetAuthorDetailsHandler.HandleAsync)
            .WithTags(Tags.Authors)
            .WithName("GetAuthorDetails")
            .Produces<AuthorDetailsDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);

        return app;
    }
}