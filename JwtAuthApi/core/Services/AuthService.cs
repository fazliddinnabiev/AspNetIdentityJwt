using JwtAuthApi.core.Dtos;
using JwtAuthApi.core.Interfaces;

namespace JwtAuthApi.core.Services;

/// <summary>
/// 
/// </summary>
public class AuthService : IAuthService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="registrationDetails"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task RegisterUserAsync(RegistrationDto registrationDetails)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logInDetails"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<string> LogInAsync(LogInDto logInDetails)
    {
        throw new NotImplementedException();
    }
}