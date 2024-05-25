using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

/// <summary>
/// Represents a service operation result that indicates a bad request.
/// This class inherits from the <see cref="ServiceResult{T}"/> base class.
/// </summary>
/// <typeparam name="T">The type of data returned by the service operation.</typeparam>
public class BadRequestResult<T> : ServiceResult<T>
{
    private readonly string _message;

    /// <summary>
    /// Initializes a new instance of the BadRequestResult class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the bad request.</param>
    public BadRequestResult(string message)
    {
        _message = message;
    }

    /// <inheritdoc />
    public override ResultTypes ResultType => ResultTypes.BadRequest;

    /// <inheritdoc />
    public override string Errors => _message;

    /// <summary>
    /// Returns default(T) because an operation failure does not produce any data.
    /// </summary>
    public override T Data => default(T);
}