namespace JwtAuthApi.core.Options
{
    /// <summary>
    /// Represents Json Web Token options.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Key used for token generation and validation.
        /// </summary>
        public required string Key { get; set; }

        /// <summary>
        /// Valid issuer of the JWT.
        /// </summary>
        public required string ValidIssuer { get; set; }

        /// <summary>
        /// Valid audience for the JWT.
        /// </summary>
        public required string ValidAudience { get; set; }
    }
}