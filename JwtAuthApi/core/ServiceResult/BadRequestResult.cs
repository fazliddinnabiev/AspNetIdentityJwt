using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

/// <summary>
/// Represents a service operation result that indicates a bad request.
/// This class inherits from the <see cref="ServiceResult{T}"/> base class.
/// </summary>
/// <typeparam name="T">The type of data returned by the service operation.</typeparam>
public class BadRequestResult<T>(string message) : ServiceResult<T>
{
    /// <inheritdoc />
    public override ResultTypes ResultType => ResultTypes.BadRequest;

    /// <inheritdoc />
    public override string Errors => message;

    /// <summary>
    /// Returns default(T) because an operation failure does not produce any data.
    /// </summary>
    public override T? Data => default(T);
}