using JwtAuthApi.core.Enums;

namespace JwtAuthApi.core.Dtos;

/// <summary>
/// Dto that is used to represent registration details.
/// </summary>
public class RegistrationDto
{
    /// <summary>
    /// Unique email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Alphanumeric characters 
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Represents user role.
    /// </summary>
    public IdentityRoles IdentityRole { get; set; }

    /// <summary>
    /// User's Full name.
    /// </summary>
    public string FullName { get; set; } = string.Empty;
}