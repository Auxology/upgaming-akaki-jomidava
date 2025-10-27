using Upgaming.Domain.Entities;
using Upgaming.Domain.Repositories;

namespace Upgaming.Infrastructure.Repositories;

/// <summary>
/// In-memory implementation of IBookRepository for demonstration purposes.
/// Uses static lists to store data across requests without requiring a database.
/// Provides ID generation for new books.
/// </summary>
internal sealed class InMemoryBookRepository : IBookRepository
{
    // Static list to persist data across instances
    private static readonly List<Book> _books;

    private static int _nextId = 4;

    // Static constructor to initialize sample data
    static InMemoryBookRepository()
    {
        _books = new List<Book>
        {
            Book.Create(1, "Clean Code", 1, 2008).Value,
            Book.Create(2, "CLR via C#", 2, 2012).Value,
            Book.Create(3, "The Clean Coder", 1, 2011).Value
        };
    }
    
    public Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        Book? book = _books.FirstOrDefault(b => b.ID == id);
        
        return Task.FromResult(book);
    }

    public Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Book> books = _books.AsReadOnly();
        
        return Task.FromResult(books);
    }

    public Task<IReadOnlyList<Book>> GetByAuthorIdAsync(int authorID, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Book> books = _books
            .Where(b => b.AuthorID == authorID)
            .ToList()
            .AsReadOnly();
        
        return Task.FromResult(books);
    }

    public Task<Book> AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        _books.Add(book);
        
        return Task.FromResult(book);
    }

    public int GenerateNextId()
    {
        return _nextId++;
    }
}