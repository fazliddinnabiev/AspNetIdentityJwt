namespace JwtAuthApi.core.Enums;

/// <summary>
/// Enum representing different types of results for operations or requests.
/// </summary>
public enum ResultTypes
{
    /// <summary>
    /// The operation or request completed successfully.
    /// </summary>
    Ok,

    /// <summary>
    /// The requested resource was not found.
    /// </summary>
    NotFound,

    /// <summary>
    /// The request contains malformed syntax and cannot be fulfilled.
    /// </summary>
    BadRequest,

    /// <summary>
    /// The request lacks valid authentication credentials.
    /// </summary>
    Unauthorized,
    
    /// <summary>
    /// Represents failure of operation.
    /// </summary>
    OperationFailed
}