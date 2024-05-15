﻿using JwtAuthApi.core.Dtos;
using JwtAuthApi.core.Interfaces;
using JwtAuthApi.core.ServiceResult;
using JwtAuthApi.extensions;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers;

/// <summary>
/// Represents endpoints related to authentication.
/// </summary>
[ApiController]
[Route("[controller]")]
public sealed class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Represents an API endpoint for user registration.
    /// </summary>
    /// <param name="registrationDetails">he registration details provided by the user.<see cref="RegistrationDto"/></param>
    /// <returns>result of registration whether succeeded or failed.</returns>
    [HttpPost]
    [Route("signUp")]
    public Task SignUp(RegistrationDto registrationDetails)
    {
        return authService.RegisterUserAsync(registrationDetails);
    }

    /// <summary>
    /// Represents an API endpoint for user sign-in.
    /// </summary>
    /// <param name="userDetails">The login details provided by the user.</param>
    /// <returns>string that represents JWT token.</returns>
    [HttpPost]
    [Route("signIn")]
    public async Task<IActionResult> SignIn(LogInDto userDetails)
    {
        var result = await authService.LogInAsync(userDetails);
        return this.FromResult(result: result);
    }
}