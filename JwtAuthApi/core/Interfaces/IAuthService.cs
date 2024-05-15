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
    /// <returns>True if registration is successful; otherwise, false.</returns>
    Task<BaseResult<bool>> RegisterUserAsync(RegistrationDto registrationDetails);

    /// <summary>
    /// Logs in a user asynchronously.
    /// </summary>
    /// <param name="logInDetails">User login details. See <see cref="LogInDto"/> for more information.</param>
    /// <returns>A JWT token upon successful login.</returns>
    Task<BaseResult<string>> LogInAsync(LogInDto logInDetails);
}