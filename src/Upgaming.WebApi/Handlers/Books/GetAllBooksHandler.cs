using Upgaming.Application.Books.Queries;
using Upgaming.Application.DTOs;
using Upgaming.SharedKernel;
using Upgaming.WebApi.Infrastructure;

namespace Upgaming.WebApi.Handlers.Books;

/// <summary>
/// Handles the request to retrieve all books with their author names.
/// </summary>
internal sealed class GetAllBooksHandler
{
    /// <summary>
    /// Retrieves all books from the catalog with their associated author information.
    /// </summary>
    /// <param name="query">The query handler for retrieving all books.</param>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    /// <returns>
    /// An OK result with a list of BookDto if successful,
    /// or a Problem Details response if the operation fails.
    /// If exception happens middleware will take care of it
    /// </returns>
    public static async Task<IResult> HandleAsync(
        GetAllBooksQuery query,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        Result<IReadOnlyList<BookDto>> result = await query.HandleAsync(cancellationToken);

        return result.IsSuccess 
            ? Results.Ok(result.Value) 
            : CustomResults.Problem(result, httpContext);
    }
}