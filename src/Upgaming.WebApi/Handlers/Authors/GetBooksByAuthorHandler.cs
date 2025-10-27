using Upgaming.Application.Books.Queries;
using Upgaming.Application.DTOs;
using Upgaming.SharedKernel;
using Upgaming.WebApi.Infrastructure;

namespace Upgaming.WebApi.Handlers.Authors;

/// <summary>
/// Handles the request to retrieve all books written by a specific author.
/// </summary>
internal sealed class GetBooksByAuthorHandler
{
    /// <summary>
    /// Retrieves all books for the specified author from the catalog.
    /// </summary>
    /// <param name="id">The unique identifier of the author.</param>
    /// <param name="query">The query handler for retrieving books by author.</param>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    /// <returns>
    /// An OK result with a list of BookDto if successful,
    /// or a Problem Details response with 404 Not Found if the author doesn't exist.
    /// </returns>
    public static async Task<IResult> HandleAsync(
        int id,
        GetBooksByAuthorQuery query,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        Result<IReadOnlyList<BookDto>> result = await query.HandleAsync(id, cancellationToken);
        
        return result.IsSuccess 
            ? Results.Ok(result.Value) 
            : CustomResults.Problem(result, httpContext);
    }
}