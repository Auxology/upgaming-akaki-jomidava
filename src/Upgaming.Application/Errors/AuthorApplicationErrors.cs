using Upgaming.SharedKernel;

namespace Upgaming.Application.Errors;

internal static class AuthorApplicationErrors
{
    public static Error NotFound => Error.NotFound
    (
        "Authors.NotFound",
        "Author with that id was not found."
    );
}