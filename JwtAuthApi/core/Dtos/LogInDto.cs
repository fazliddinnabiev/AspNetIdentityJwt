namespace JwtAuthApi.core.Dtos;

/// <summary>
/// Dto that represents User credentials to lo in.
/// </summary>
public class LogInDto
{
    /// <summary>
    /// Email address of a user.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Password of a user
    /// </summary>
    public string Password { get; set; } = string.Empty;
}