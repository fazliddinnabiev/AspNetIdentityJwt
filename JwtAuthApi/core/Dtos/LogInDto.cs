namespace JwtAuthApi.core.Dtos;

/// <summary>
/// Dto that represents User credentials to log in.
/// </summary>
public class LogInDto
{
    /// <summary>
    /// Email address of a user.
    /// </summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>
    /// Password of a user.
    /// </summary>
    public string Password { get; init; } = string.Empty;
}