using JwtAuthApi.core.Dtos;
using JwtAuthApi.core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthApi.core.Services;

/// <summary>
/// 
/// </summary>
public class AuthService(UserManager<IdentityUser> userManager) : IAuthService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="registrationDetails"></param>
    /// <returns></returns>
    public async Task<bool> RegisterUserAsync(RegistrationDto registrationDetails)
    {
        IdentityUser user = new IdentityUser
        {
            UserName = registrationDetails.Email,
            Email = registrationDetails.Email,
        };

        var result = await userManager.CreateAsync(user, registrationDetails.Password);

        return result.Succeeded;
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