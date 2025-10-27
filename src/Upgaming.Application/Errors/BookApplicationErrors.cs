using Upgaming.SharedKernel;

namespace Upgaming.Application.Errors;

internal static class BookApplicationErrors
{
    
    public static Error MustHaveAuthor => Error.Validation
    (
        code: "Books.MustHaveAuthor",
        description: "Book must have an author."
    );
}