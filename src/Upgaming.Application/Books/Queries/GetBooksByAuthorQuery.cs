using Upgaming.Application.DTOs;
using Upgaming.Application.Errors;
using Upgaming.Domain.Entities;
using Upgaming.Domain.Repositories;
using Upgaming.SharedKernel;

namespace Upgaming.Application.Books.Queries;

internal sealed class GetBooksByAuthorQuery
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public GetBooksByAuthorQuery(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    public async Task<Result<IReadOnlyList<BookDto>>> HandleAsync(int authorID,
        CancellationToken cancellationToken = default)
    {
        Author? author = await _authorRepository.GetByIdAsync(authorID, cancellationToken);

        if (author is null)
            return Result.Failure<IReadOnlyList<BookDto>>(AuthorApplicationErrors.NotFound);
        
        IReadOnlyList<Book> books = await _bookRepository.GetByAuthorIdAsync(authorID, cancellationToken);

        List<BookDto> bookDtos = books.Select(book => new BookDto
            {
                ID = book.ID,
                Title = book.Title,
                AuthorName = author.Name,
                PublicationYear = book.PublicationYear
            })
            .ToList();
        
        return Result.Success<IReadOnlyList<BookDto>>(bookDtos);
    }
}