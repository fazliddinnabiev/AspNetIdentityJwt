using JwtAuthApi.core.Dtos;
using JwtAuthApi.core.ServiceResult;

namespace JwtAuthApi.core.Interfaces;

/// <summary>
/// Represents an interface for authentication services.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a user asynchronously.
    /// </summary>
    /// <param name="registrationDetails">User registration details. See <see cref="RegistrationDto"/> for more information.</param>
    /// <param name="cancellationToken">A token that can be used to request cancellation of the asynchronous operation.</param>
    /// <returns>True if registration is successful; otherwise, false.</returns>
    Task<ServiceResult<bool>> RegisterUserAsync(RegistrationDto registrationDetails, CancellationToken cancellationToken = default);

    /// <summary>
    /// Authenticates a user asynchronously.
    /// </summary>
    /// <param name="logInDetails">User login details. See <see cref="LogInDto"/> for more information.</param>
    /// <param name="cancellationToken">A token that can be used to request cancellation of the asynchronous operation.</param>
    /// <returns>A JWT token upon successful login.</returns>
    Task<ServiceResult<string>> AuthenticateAsync(LogInDto logInDetails, CancellationToken cancellationToken = default);
}