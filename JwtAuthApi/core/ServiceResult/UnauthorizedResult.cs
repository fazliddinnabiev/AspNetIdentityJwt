using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

/// <summary>
/// Represents a service operation result that indicates an unauthorized request.
/// This class inherits from the <see cref="ServiceResult{T}"/> base class.
/// </summary>
/// <typeparam name="T">The type of data returned by the service operation.</typeparam>
public class UnauthorizedResult<T> : ServiceResult<T>
{
    private string _message;

    /// <summary>
    /// Initializes a new instance of the UnauthorizedResult class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the unauthorized request.</param>
    public UnauthorizedResult(string message)
    {
        _message = message;
    }

    /// <inheritdoc />
    public override ResultTypes ResultType => ResultTypes.Unauthorized;

    /// <inheritdoc />
    public override string Errors => _message;

    /// <summary>
    /// Returns default(T) because an unauthorized request does not produce any data.
    /// </summary>
    public override T Data => default(T);
}