using Upgaming.Domain.Entities;

namespace Upgaming.Domain.Repositories;

public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<Author>> GetAllAsync(CancellationToken cancellationToken = default);
}