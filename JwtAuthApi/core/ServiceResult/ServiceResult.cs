using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

/// <summary>
/// Represents the result of a service operation.
/// </summary>
/// <typeparam name="T">The type of data returned by the service operation.</typeparam>
public abstract class ServiceResult<T>
{
    /// <summary>
    /// Gets the type of the result. See <see cref="ResultTypes"/> for possible values.
    /// </summary>
    public abstract ResultTypes ResultType { get; }

    /// <summary>
    /// Gets the error message related to an failure.
    /// </summary>
    public abstract string Errors { get; }

    /// <summary>
    /// Gets the data produced by the service operation.
    /// Returns default(T) if the operation was not successful.
    /// </summary>
    public abstract T Data { get; }
}