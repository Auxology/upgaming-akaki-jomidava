using Upgaming.Domain.Entities;

namespace Upgaming.Domain.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<Book>> GetByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default);
    
    Task<Book> AddAsync(Book book, CancellationToken cancellationToken = default);
}