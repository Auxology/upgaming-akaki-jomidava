using Upgaming.Application.DTOs;
using Upgaming.Application.Errors;
using Upgaming.Domain.Entities;
using Upgaming.Domain.Repositories;
using Upgaming.SharedKernel;

namespace Upgaming.Application.Books.Queries;

/// <summary>
/// Query handler for retrieving all books with their associated author information.
/// Validates data integrity by ensuring each book has a valid author reference,
/// returning a failure result if any book lacks an author (domain violation).
/// </summary>
internal sealed class GetAllBooksQuery
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    
    public GetAllBooksQuery(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    public async Task<Result<IReadOnlyList<BookDto>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Book> books = await _bookRepository.GetAllAsync(cancellationToken);
        IReadOnlyList<Author> authors = await _authorRepository.GetAllAsync(cancellationToken);

        List<BookDto> bookDtos = new();

        foreach (Book book in books)
        {
            Author? author = authors.FirstOrDefault(a => a.ID == book.AuthorID);

            // Validate data integrity - all books must have authors (domain rule)
            if (author is null)
                return Result.Failure<IReadOnlyList<BookDto>>(BookApplicationErrors.MustHaveAuthor);

            bookDtos.Add(new BookDto
            {
                ID = book.ID,
                Title = book.Title,
                AuthorName = author.Name,
                PublicationYear = book.PublicationYear
            });
        }

        return Result.Success<IReadOnlyList<BookDto>>(bookDtos);
    }
}