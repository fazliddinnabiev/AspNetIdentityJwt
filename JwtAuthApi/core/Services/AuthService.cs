using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using JwtAuthApi.core.Dtos;
using JwtAuthApi.core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthApi.core.Services;

/// <summary>
/// Represents the authentication service for user
/// </summary>
public class AuthService(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration) : IAuthService
{
    /// <summary>
    /// Registers user with provided user details.
    /// </summary>
    /// <param name="registrationDetails">User details.</param>
    /// <returns>True if registration succeeds, otherwise false.</returns>
    public async Task<bool> RegisterUserAsync(RegistrationDto registrationDetails)
    {
        IdentityUser user = new IdentityUser
        {
            UserName = registrationDetails.Email,
            Email = registrationDetails.Email,
        };

        var result = await userManager.CreateAsync(user, registrationDetails.Password);
        var roleResult = await userManager.AddToRoleAsync(user: user, registrationDetails.UserRole.ToString());
        return result.Succeeded;
    }

    /// <summary>
    /// Authenticates user base on the provided credentials.
    /// </summary>
    /// <param name="logInDetails">User credentials</param>
    /// <returns>JWT token if authentication succeeds.</returns>
    public async Task<string> LogInAsync(LogInDto logInDetails)
    {
        IdentityUser? user = await userManager.FindByEmailAsync(logInDetails.Username);
        if (user is null)
        {
            throw new Exception("Email or password is incorrect.");
        }

        bool isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user: user);
        if (!isEmailConfirmed)
        {
            throw new Exception("Email is not confirmed");
        }

        bool passwordMatch = await userManager.CheckPasswordAsync(user: user, logInDetails.Password);
        if (!passwordMatch)
        {
            throw new Exception("Email or password is incorrect.");
        }

        return GenerateJwtAsync(user);
    }

    /// <summary>
    /// Generates a JWT token for the authenticated user.
    /// <param name="user">Authenticated user.</param>
    /// </summary>
    /// <returns>JWT token.</returns>
    private string GenerateJwtAsync(IdentityUser user)
    {
        var jwtKey = configuration.GetSection("JWT:Key").Value;
        if (jwtKey is null)
        {
            throw new ArgumentNullException(nameof(jwtKey));
        }

        var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKey);
        var symmetricKey = new SymmetricSecurityKey(jwtKeyBytes);
        var signingKeyCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim("userId", user.Id),
            new Claim("userName", user.UserName)
        };

        var token = new JwtSecurityToken(
            issuer: configuration.GetSection("JWT:ValidIssuer").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signingKeyCredentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}