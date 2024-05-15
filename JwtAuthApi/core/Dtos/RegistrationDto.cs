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
    public required string Email { get; init; }

    /// <summary>
    /// Alphanumeric characters.
    /// </summary>
    public required string Password { get; init; }

    /// <summary>
    /// Represents user role.
    /// </summary>
    public required IdentityRoles IdentityRole { get; init; }
}