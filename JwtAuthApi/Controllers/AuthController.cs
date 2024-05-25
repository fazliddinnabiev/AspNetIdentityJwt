using JwtAuthApi.core.Dtos;
using JwtAuthApi.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers;

/// <summary>
/// Represents endpoints related to authentication.
/// </summary>
[ApiController]
[Route("[controller]")]
public sealed class AuthController(IAuthService authService) : ApiController
{
    /// <summary>
    /// Represents an API endpoint for user registration.
    /// </summary>
    /// <param name="registrationDetails">he registration details provided by the user.<see cref="RegistrationDto"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns>result of registration whether succeeded or failed.</returns>
    [HttpPost]
    [Route("signUp")]
    public async Task<ActionResult<bool>> SignUp([FromBody] RegistrationDto registrationDetails, CancellationToken cancellationToken = default)
    {
        var result = await authService.RegisterUserAsync(registrationDetails: registrationDetails, cancellationToken: cancellationToken);
        return this.ToActionResult(result: result);
    }

    /// <summary>
    /// Represents an API endpoint for user sign-in.
    /// </summary>
    /// <param name="userDetails">The login details provided by the user.</param>
    /// <param name="cancellationToken">A token that can be used to request cancellation of the asynchronous operation.</param>
    /// <returns>string that represents JWT token.</returns>
    [HttpPost("signIn")]
    public async Task<ActionResult<string>> SignIn([FromBody] LogInDto userDetails, CancellationToken cancellationToken = default)
    {
        var result = await authService.AuthenticateAsync(userDetails, cancellationToken);
        return this.ToActionResult(result: result);
    }
}