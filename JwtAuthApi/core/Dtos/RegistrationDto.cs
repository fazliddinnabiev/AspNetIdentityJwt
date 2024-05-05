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
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Alphanumeric characters.
    /// </summary>
    public string Password { get; init; } = string.Empty;

    /// <summary>
    /// Represents user role.
    /// </summary>
    public IdentityRoles IdentityRole { get; init; }

    /// <summary>
    /// User's Full name.
    /// </summary>
    public string FullName { get; init; } = string.Empty;
}