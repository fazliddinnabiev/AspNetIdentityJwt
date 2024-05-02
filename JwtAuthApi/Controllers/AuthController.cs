using JwtAuthApi.core.Dtos;
using JwtAuthApi.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Register user with given information.
    /// </summary>
    /// <param name="registrationDetails"><see cref="RegistrationDto"/></param>
    /// <returns></returns>
    [HttpPost]
    [Route("signUp")]
    public Task SignUp(RegistrationDto registrationDetails)
    {
        return authService.RegisterUserAsync(registrationDetails);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userDetails"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("signIn")]
    public Task SignIn(LogInDto userDetails)
    {
        authService.LogInAsync(userDetails);
        throw new NotImplementedException();
    }
}