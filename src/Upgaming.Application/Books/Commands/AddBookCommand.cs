using Upgaming.Application.DTOs;
using Upgaming.Application.Errors;
using Upgaming.Domain.Entities;
using Upgaming.Domain.Repositories;
using Upgaming.SharedKernel;

namespace Upgaming.Application.Books.Commands;

/// <summary>
/// Command handler for adding a new book to the catalog.
/// Performs business validation and orchestrates the book creation process.
/// </summary>
public sealed class AddBookCommand
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    
    public AddBookCommand(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    /// <summary>
    /// Handles the request to add a new book with validation.
    /// </summary>
    /// <param name="request">The book creation request containing title, author ID, and publication year.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    /// <returns>A Result containing the created BookDto if successful, or an Error if validation fails.</returns>
    public async Task<Result<BookDto>> HandleAsync(
        CreateBookRequest request,
        CancellationToken cancellationToken = default)
    {
        // Business validation: Title cannot be empty (checked at application level for better error messages)
        if (string.IsNullOrWhiteSpace(request.Title))
            return Result.Failure<BookDto>(BookApplicationErrors.TitleRequired);
        
        // Business validation: Author must exist in the system
        Author? author = await _authorRepository.GetByIdAsync(request.AuthorID, cancellationToken);
        if (author is null)
            return Result.Failure<BookDto>(AuthorApplicationErrors.NotFound);

        int newId = _bookRepository.GenerateNextId();
        
        // Business validation: Publication year cannot be in the future
        int currentYear = DateTime.UtcNow.Year;
        
        if (request.PublicationYear > currentYear)
            return Result.Failure<BookDto>(BookApplicationErrors.InvalidYear);

        // Create book entity using factory method with domain validation
        Result<Book> newBookResult = Book.Create
        (
            id: newId,
            title: request.Title,
            authorID: request.AuthorID,
            publicationYear: request.PublicationYear
        );

        if (newBookResult.IsFailure)
            return Result.Failure<BookDto>(newBookResult.Error);

        Book newBook = newBookResult.Value;
        
        Book createdBook = await _bookRepository.AddAsync(newBook, cancellationToken);

        // Map entity to DTO for API response
        BookDto bookDto = new()
        {
            ID = createdBook.ID,
            Title = createdBook.Title,
            AuthorName = author.Name,
            PublicationYear = createdBook.PublicationYear
        };

        return Result.Success(bookDto);    
    }
}