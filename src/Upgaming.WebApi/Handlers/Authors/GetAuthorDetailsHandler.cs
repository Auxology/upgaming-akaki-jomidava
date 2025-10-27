using Upgaming.Application.Authors.Queries;
using Upgaming.Application.DTOs;
using Upgaming.SharedKernel;
using Upgaming.WebApi.Infrastructure;

namespace Upgaming.WebApi.Handlers.Authors;

/// <summary>
/// Handles the request to retrieve author details with their complete list of books.
/// Bonus feature (Part 3-C): Returns author with nested book collection.
/// </summary>
internal sealed class GetAuthorDetailsHandler
{
    /// <summary>
    /// Retrieves detailed information about an author including all books they have written.
    /// </summary>
    /// <param name="id">The unique identifier of the author.</param>
    /// <param name="query">The query handler for retrieving author details.</param>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    /// <returns>
    /// An OK result with AuthorDetailsDto containing author information and nested books if successful,
    /// or a Problem Details response with 404 Not Found if the author doesn't exist.
    /// </returns>
    public static async Task<IResult> HandleAsync(
        int id,
        GetAuthorDetailsQuery query,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        Result<AuthorDetailsDto> result = await query.HandleAsync(id, cancellationToken);
        
        return result.IsSuccess 
            ? Results.Ok(result.Value) 
            : CustomResults.Problem(result, httpContext);
    }
}