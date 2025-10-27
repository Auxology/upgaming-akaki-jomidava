namespace Upgaming.Application.DTOs;

/// <summary>
/// Request model for creating a new book.
/// Contains the required data to add a book to the catalog.
/// </summary>
public sealed record CreateBookRequest
{
    public string Title { get; init; } = string.Empty;
    
    public int AuthorID { get; init; }
    
    public int PublicationYear { get; init; }
}