using Upgaming.SharedKernel;

namespace Upgaming.Domain.Errors;

internal static class AuthorDomainErrors
{
    public static Error InvalidID => Error.Validation
    (
        "Authors.InvalidID",
        "ID must be greater than zero."
    );

    public static Error NameRequired => Error.Validation
    (
        "Authors.NameRequired",
        "Name is required."
    );
}