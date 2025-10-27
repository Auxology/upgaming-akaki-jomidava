namespace Upgaming.Application.DTOs;

/// <summary>
/// Data Transfer Object representing a book with its author's name.
/// Used to shape API responses.
/// </summary>
public sealed record BookDto
{
    public int ID { get; init; }
    
    public string Title { get; init; } = string.Empty;
    
    public string AuthorName { get; init; } = string.Empty;
    
    public int PublicationYear { get; init; }
}