namespace Upgaming.SharedKernel;

/// <summary>
/// Represents an error in the application with structured information including code, description, and type.
/// This immutable record struct follows the functional programming approach for error handling,
/// providing a consistent way to represent and categorize different types of errors throughout the application.
/// </summary>
/// <param name="Code">A unique identifier for the error, typically following a hierarchical naming convention (e.g., "User.NotFound", "Validation.Email.Invalid")</param>
/// <param name="Description">A human-readable description of the error that can be displayed to users or logged for debugging purposes</param>
/// <param name="Type">The category of error that determines how it should be handled and what HTTP status code should be returned</param>
/// You can always create new errors on-demand.
public readonly record struct Error(string Code, string Description, ErrorType Type)
{
    public static Error None => new(Code: string.Empty, Description: string.Empty, ErrorType.Failure);

    public static Error NullValue => new
    (
        Code: "General.NullValue",
        Description: "A null value was provided.",
        ErrorType.Failure
    );

    public static Error Failure(string code, string description)
        => new(code, description, ErrorType.Failure);

    public static Error Validation(string code, string description)
        => new(code, description, ErrorType.Validation);

    public static Error Problem(string code, string description)
        => new(code, description, ErrorType.Problem);

    public static Error NotFound(string code, string description)
        => new(code, description, ErrorType.NotFound);

    public static Error Conflict(string code, string description)
        => new(code, description, ErrorType.Conflict);

    public static Error Unauthorized(string code, string description)
        => new(code, description, ErrorType.Unauthorized);
}