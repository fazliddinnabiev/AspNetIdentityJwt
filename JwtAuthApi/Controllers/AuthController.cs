using JwtAuthApi.core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers;

/// <summary>
/// 
/// </summary>

[ApiController]
[Route("[controller]")]
public class AuthController() : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="registrationDetails"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Route("signUp")]
    public Task SignUp(RegistrationDto registrationDetails)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}