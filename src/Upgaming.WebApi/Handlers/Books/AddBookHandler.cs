using Upgaming.Application.Books.Commands;
using Upgaming.Application.DTOs;
using Upgaming.SharedKernel;
using Upgaming.WebApi.Infrastructure;

namespace Upgaming.WebApi.Handlers.Books;

/// <summary>
/// Handles the request to add a new book to the catalog.
/// Performs validation and creates the book if all requirements are met.
/// </summary>
internal sealed class AddBookHandler
{
    /// <summary>
    /// Creates a new book in the catalog with validation.
    /// Validates that the title is not empty, the publication year is not in the future,
    /// and the author exists in the system.
    /// </summary>
    /// <param name="createBookRequest">The request containing book details (title, author ID, publication year).</param>
    /// <param name="command">The command handler for adding a new book.</param>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    /// <returns>
    /// A 201 Created result with the created BookDto and location header if successful,
    /// or a Problem Details response with 400 Bad Request if validation fails.
    /// </returns>
    public static async Task<IResult> HandleAsync(
        CreateBookRequest createBookRequest,
        AddBookCommand command,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        Result<BookDto> result = await command.HandleAsync(createBookRequest, cancellationToken);
        
        return result.IsSuccess 
            ? Results.Created($"/api/books/{result.Value.ID}", result.Value) 
            : CustomResults.Problem(result, httpContext);
    }
}