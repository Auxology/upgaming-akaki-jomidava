using Upgaming.Application.DTOs;
using Upgaming.WebApi.Handlers.Books;

namespace Upgaming.WebApi.Endpoints;

/// <summary>
/// Configures all book-related API endpoints and maps them to their respective handlers.
/// </summary>
internal static class BookEndpoints
{
    public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/books", GetAllBooksHandler.HandleAsync)
            .WithTags(Tags.Books)
            .WithName("GetAllBooks")
            .Produces<IReadOnlyList<BookDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        app.MapPost("/api/books", AddBookHandler.HandleAsync)
            .WithTags(Tags.Books)
            .WithName("AddBook")
            .Produces<BookDto>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        return app;
    }
}