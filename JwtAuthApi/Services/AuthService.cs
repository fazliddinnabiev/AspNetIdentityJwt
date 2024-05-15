using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthApi.core.Dtos;
using JwtAuthApi.core.Interfaces;
using JwtAuthApi.core.ServiceResult;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthApi.Services;

/// <inheritdoc/>
public class AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration) : IAuthService
{
    /// <inheritdoc/>
    public async Task<ServiceResult<bool>> RegisterUserAsync(RegistrationDto registrationDetails, CancellationToken cancellationToken)
    {
        IdentityUser user = new IdentityUser
        {
            UserName = registrationDetails.Email,
            Email = registrationDetails.Email,
        };

        var result = await userManager.CreateAsync(user, registrationDetails.Password);
        await userManager.AddToRoleAsync(user: user, registrationDetails.IdentityRole.ToString());
        return new SuccessResult<bool>(data: true);
    }

    /// <inheritdoc/>
    public async Task<ServiceResult<string>> LogInAsync(LogInDto logInDetails, CancellationToken cancellationToken)
    {
        IdentityUser? user = await userManager.FindByEmailAsync(logInDetails.Username);
        if (user is null)
        {
            return new NotFoundResult<string>(message: "You do not have an account.");
        }

        bool isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user: user);
        if (!isEmailConfirmed)
        {
            return new UnauthorizedResult<string>(message: "Account is not confirmed.");
        }

        bool passwordMatch = await userManager.CheckPasswordAsync(user: user, logInDetails.Password);
        if (!passwordMatch)
        {
            return new UnauthorizedResult<string>("Password or email is not correct.");
        }

        var userRoles = await userManager.GetRolesAsync(user);

        var jwtToken = GenerateJwtAsync(user, userRoles);

        return new SuccessResult<string>(jwtToken);
    }

    private string GenerateJwtAsync(IdentityUser user, IList<string> userRoles)
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

        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = new JwtSecurityToken(
            issuer: configuration.GetSection("JWT:ValidIssuer").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signingKeyCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}