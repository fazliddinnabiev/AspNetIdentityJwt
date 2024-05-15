using System.ComponentModel.DataAnnotations;

namespace JwtAuthApi.core.Dtos;

/// <summary>
/// Dto that represents User credentials to log in.
/// </summary>
public class LogInDto
{
    /// <summary>
    /// Email address of a user.
    /// </summary>
    [Required]
    [MinLength(1)]
    public required string Username { get; init; }

    /// <summary>
    /// Password of a user.
    /// </summary>
    [Required]
    [MinLength(1)]
    public required string Password { get; init; }
}