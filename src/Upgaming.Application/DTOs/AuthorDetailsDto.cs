namespace Upgaming.Application.DTOs;

/// <summary>
/// Data Transfer Object representing an author with their complete list of books.
/// Used for the bonus endpoint that returns author details with nested books.
/// </summary>
public sealed record AuthorDetailsDto
{
    public int ID { get; init; }
    
    public string Name { get; init; } = string.Empty;
    
    public IReadOnlyList<BookSummaryDto> Books { get; init; } = Array.Empty<BookSummaryDto>();
}

/// <summary>
/// Simplified book information for nested display within author details.
/// </summary>
public sealed record BookSummaryDto
{
    public int ID { get; init; }
    
    public string Title { get; init; } = string.Empty;
    
    public int PublicationYear { get; init; }
}