using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

/// <summary>
/// Represents a service operation result that indicates a resource was not found.
/// This class inherits from the <see cref="ServiceResult{T}"/> base class.
/// </summary>
/// <typeparam name="T">The type of data returned by the service operation.</typeparam>
public class NotFoundResult<T>(string message) : ServiceResult<T>
{
    /// <summary>
    /// Gets the error message associated with the resource not being found.
    /// </summary>
    public override ResultTypes ResultType => ResultTypes.NotFound;

    /// <inheritdoc />
    public override string Errors => message;

    /// <summary>
    /// Returns default(T) because a resource not found does not produce any data.
    /// </summary>
    public override T? Data => default(T);
}