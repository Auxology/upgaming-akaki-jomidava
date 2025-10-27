namespace Upgaming.SharedKernel;

/// <summary>
/// Represents the result of an operation that can either succeed or fail.
/// This class implements the Result pattern, providing a functional approach to error handling
/// that eliminates the need for exceptions in business logic and makes error handling explicit.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the Result class.
    /// This protected constructor is used by derived classes and static factory methods.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful</param>
    /// <param name="error">The error information if the operation failed, or Error.None if successful</param>
    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public bool IsSuccess { get; }
    
    public bool IsFailure => !IsSuccess;
    
    public Error Error { get; }
    
    public static Result Success() => new(isSuccess: true, Error.None);
    
    public static Result<T> Success<T>(T value) => new(value, isSuccess: true, Error.None);
    
    public static Result Failure(Error error) => new(isSuccess: false, error);
    
    public static Result<T> Failure<T>(Error error) => new(default, isSuccess: false, error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    /// <summary>
    /// Initializes a new instance of the Result&lt;T&gt; class.
    /// </summary>
    /// <param name="value">The value to be returned if the operation succeeded, or default(T) if it failed</param>
    /// <param name="isSuccess">Indicates whether the operation was successful</param>
    /// <param name="error">The error information if the operation failed, or Error.None if successful</param>
    public Result(T? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }
    
    public T Value => IsSuccess && _value is not null
        ? _value
        : throw new InvalidOperationException("Cannot access the value of a failure result.");
}