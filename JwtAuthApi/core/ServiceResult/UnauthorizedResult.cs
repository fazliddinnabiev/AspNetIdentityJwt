using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

/// <summary>
/// Represents a service operation result that indicates an unauthorized request.
/// This class inherits from the <see cref="ServiceResult{T}"/> base class.
/// </summary>
/// <typeparam name="T">The type of data returned by the service operation.</typeparam>
public class UnauthorizedResult<T>(string message) : ServiceResult<T>
{
    /// <inheritdoc />
    public override ResultTypes ResultType => ResultTypes.Unauthorized;

    /// <inheritdoc />
    public override string Errors => message;

    /// <summary>
    /// Returns default(T) because an unauthorized request does not produce any data.
    /// </summary>
    public override T? Data => default(T);
}