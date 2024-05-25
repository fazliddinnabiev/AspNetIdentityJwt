using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.ServiceResult;

/// <summary>
/// Represents a service operation result that indicates a successful operation.
/// This class inherits from the <see cref="ServiceResult{T}"/> base class.
/// </summary>
/// <typeparam name="T">The type of data returned by the service operation.</typeparam>
public class SuccessResult<T> : ServiceResult<T>
{
    private readonly T _data;

    /// <summary>
    /// Initializes a new instance of the SuccessResult class with the specified data.
    /// </summary>
    /// <param name="data">The data produced by the successful.</param>
    public SuccessResult(T data)
    {
        _data = data;
    }

    /// <summary>
    /// Returns an empty string because a successful operation does not produce any errors.
    /// </summary>
    public override ResultTypes ResultType => ResultTypes.Ok;

    /// <inheritdoc />
    public override string Errors => string.Empty;

    /// <summary>
    /// Gets the data produced by the successful service operation.
    /// </summary>
    public override T Data => _data;
}