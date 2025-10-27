using Upgaming.Domain.Entities;
using Upgaming.Domain.Repositories;

namespace Upgaming.Infrastructure.Repositories;

/// <summary>
/// In-memory implementation of IAuthorRepository for demonstration purposes.
/// Uses static lists to store data across requests without requiring a database.
/// </summary>
internal sealed class InMemoryAuthorRepository : IAuthorRepository
{
    // Static list to persist data across instances
    private static readonly List<Author> _authors;

    // Static constructor to initialize sample data
    static InMemoryAuthorRepository()
    {
        _authors = new List<Author>
        {
            Author.Create(1, "Robert C. Martin").Value,
            Author.Create(2, "Jeffrey Richter").Value
        };
    }
    
    public Task<Author?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        Author? author = _authors.FirstOrDefault(a => a.ID == id);
        return Task.FromResult(author);
    }

    public Task<IReadOnlyList<Author>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Author> authors = _authors.AsReadOnly();
        
        return Task.FromResult(authors);
    }
}