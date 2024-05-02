using JwtAuthApi.core.Dtos;

namespace JwtAuthApi.core.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="registrationDetails"></param>
    /// <returns></returns>
    Task RegisterUserAsync(RegistrationDto registrationDetails);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logInDetails"></param>
    /// <returns></returns>
    Task<string> LogInAsync (LogInDto logInDetails);
}