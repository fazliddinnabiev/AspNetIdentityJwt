using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

/// <summary>
/// Represents a service operation result that indicates an operation has failed.
/// This class inherits from the <see cref="ServiceResult{T}"/> base class.
/// </summary>
/// <typeparam name="T">The type of data returned by the service operation.</typeparam>
public class OperationFailedResult<T> : ServiceResult<T>
{
    /// <inheritdoc />
    public override ResultTypes ResultType => ResultTypes.OperationFailed;

    /// <inheritdoc />
    public override string Errors { get; }

    /// <summary>
    /// Returns default(T) because an operation failure does not produce any data.
    /// </summary>
    public override T Data { get; }
}