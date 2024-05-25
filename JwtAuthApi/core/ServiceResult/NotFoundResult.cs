using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

/// <summary>
/// Represents a service operation result that indicates a resource was not found.
/// This class inherits from the <see cref="ServiceResult{T}"/> base class.
/// </summary>
/// <typeparam name="T">The type of data returned by the service operation.</typeparam>
public class NotFoundResult<T> : ServiceResult<T>
{
    private readonly string _message;

    /// <summary>
    /// Initializes a new instance of the NotFoundResult class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the resource not being found.</param>
    public NotFoundResult(string message)
    {
        _message = message;
    }

    /// <summary>
    /// Gets the error message associated with the resource not being found.
    /// </summary>
    public override ResultTypes ResultType => ResultTypes.NotFound;

    /// <inheritdoc />
    public override string Errors => _message;

    /// <summary>
    /// Returns default(T) because a resource not found does not produce any data.
    /// </summary>
    public override T Data => default(T);
}