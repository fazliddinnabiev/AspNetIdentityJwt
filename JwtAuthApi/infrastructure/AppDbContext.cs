using JwtAuthApi.infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthApi.infrastructure;

/// <summary>
/// Represents the application database context.
/// </summary>
/// <param name="options">The options for configuring the context.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options: options)
{
    /// <summary>
    /// Configures the model for the database context during entity framework migrations.
    /// </summary>
    /// <param name="modelBuilder">The model builder instance.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfigurations());
    }
}