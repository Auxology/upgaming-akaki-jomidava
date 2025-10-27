using Upgaming.SharedKernel;

namespace Upgaming.Domain.Errors;

internal static class BookDomainErrors
{
    public static Error TitleRequired => Error.Validation
    (
        "Books.TitleRequired",
        "A title is required for a book."
    );
    public static Error InvalidBookID => Error.Validation
    (
        "Books.InvalidBookID",
        "Book ID must be greater than zero."
    );
    
    public static Error InvalidAuthorID => Error.Validation
    (
        "Books.InvalidAuthorID",
        "Author ID must be greater than zero."
    );
}