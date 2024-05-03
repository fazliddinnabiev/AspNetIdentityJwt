using JwtAuthApi.core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtAuthApi.infrastructure.Configurations;

/// <summary>
/// Represents role configurations.
/// </summary>
public sealed class RoleConfigurations : IEntityTypeConfiguration<IdentityRole>
{
    /// <summary>
    /// configures Identity roles and seeds data.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        string[] userRoles = Enum.GetNames(typeof(UserRoles));
        IdentityRole[] identityRoles = GetRoles(userRoles);
        builder.HasData(identityRoles);
    }

    private IdentityRole[] GetRoles(string[] roleNames)
    {
        IdentityRole[] roles = new IdentityRole[roleNames.Length];
        for (int index = 0; index < roleNames.Length; index++)
        {
            roles[index] = new IdentityRole
            {
                Name = roleNames[index],
                NormalizedName = roleNames[index].Normalize()
            };
        }

        return roles;
    }
}