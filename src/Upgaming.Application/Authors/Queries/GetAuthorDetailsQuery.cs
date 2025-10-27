using Upgaming.Application.DTOs;
using Upgaming.Application.Errors;
using Upgaming.Domain.Entities;
using Upgaming.Domain.Repositories;
using Upgaming.SharedKernel;

namespace Upgaming.Application.Authors.Queries;

/// <summary>
/// Query to retrieve a single author with their complete list of books.
/// Bonus feature(C): Returns author details with nested book list.
/// </summary>
public sealed class GetAuthorDetailsQuery
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorDetailsQuery(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    public async Task<Result<AuthorDetailsDto>> HandleAsync(int authorID, CancellationToken cancellation = default)
    {
        Author? author = await _authorRepository.GetByIdAsync(authorID, cancellation);

        if (author is null)
            return Result.Failure<AuthorDetailsDto>(AuthorApplicationErrors.NotFound);

        // Get all books by this author
        IReadOnlyList<Book> books = await _bookRepository.GetByAuthorIdAsync(authorID, cancellation);

        // Map books to summary dto
        IReadOnlyList<BookSummaryDto> booksSummary = books.Select(book => new BookSummaryDto
            {
                ID = book.ID,
                Title = book.Title,
                PublicationYear = book.PublicationYear
            })
            .ToList();

        AuthorDetailsDto authorDetailsDto = new()
        {
            ID = author.ID,
            Name = author.Name,
            Books = booksSummary
        };
        
        return Result.Success<AuthorDetailsDto>(authorDetailsDto);
    }
}