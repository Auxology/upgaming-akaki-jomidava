using Upgaming.SharedKernel;

namespace Upgaming.Application.Errors;

internal static class BookApplicationErrors
{
    
    public static Error MustHaveAuthor => Error.Validation
    (
        code: "Books.MustHaveAuthor",
        description: "Book must have an author."
    );

    public static Error TitleRequired => Error.Validation
    (
        "Books.TitleRequired",
        "Book title cannot be empty."
    );

    public static Error InvalidYear => Error.Validation
    (
        "Books.InvalidYear",
        "Publication year cannot be in the future."
    );
}