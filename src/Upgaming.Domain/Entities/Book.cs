using Upgaming.Domain.Errors;
using Upgaming.SharedKernel;

namespace Upgaming.Domain.Entities;

/// <summary>
/// Represents a book written by an author.
/// This entity enforces domain invariants through encapsulation and factory methods.
/// </summary>
public sealed class Book
{
    public int ID { get; private set; }
    
    public string Title { get; private set; }
    
    public int AuthorID { get; private set; }
    
    public int PublicationYear { get; private set; }
    
    private Book(int id, string title, int authorID, int publicationYear)
    {
        ID = id;
        Title = title;
        AuthorID = authorID;
        PublicationYear = publicationYear;
    }

    /// <summary>
    /// Factory method to create a new Book instance with domain validation.
    /// Validates domain invariants before creating the entity.
    /// </summary>
    /// <param name="id">The unique identifier for the book. Must be greater than 0.</param>
    /// <param name="title">The book title. Cannot be null or whitespace.</param>
    /// <param name="authorID">The author's identifier. Must be greater than 0.</param>
    /// <param name="publicationYear">The year the book was published.</param>
    /// <returns>A Result containing the created Book if successful, or an Error if validation fails.</returns>
    public static Result<Book> Create(int id, string title, int authorID, int publicationYear)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<Book>(BookDomainErrors.TitleRequired);

        if (authorID <= 0)
            return Result.Failure<Book>(BookDomainErrors.InvalidAuthorID);
    
        if (id <= 0)
            return Result.Failure<Book>(BookDomainErrors.InvalidBookID);

        Book book = new Book(id, title, authorID, publicationYear);

        return Result.Success(book);
    }
}