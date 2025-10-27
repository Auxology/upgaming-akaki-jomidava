using Upgaming.Domain.Errors;
using Upgaming.SharedKernel;

namespace Upgaming.Domain.Entities;

/// <summary>
/// Represents an author who writes books.
/// </summary>
public sealed class Author
{
    public int ID { get; private set; }
    
    public string Name { get; private set; }

    private Author(int id, string name)
    {
        ID = id;
        Name = name;
    }
    
    /// <summary>
    /// Factory method to create a new Author instance.
    /// </summary>
    public static Result<Author> Create(int id, string name)
    {
        if (id <= 0)
            return Result.Failure<Author>(AuthorDomainErrors.InvalidID);

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Author>(AuthorDomainErrors.NameRequired);
        
        Author author = new Author(id, name);
        
        return Result.Success(author);
    }
}