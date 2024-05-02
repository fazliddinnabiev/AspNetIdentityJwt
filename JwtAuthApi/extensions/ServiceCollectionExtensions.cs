using JwtAuthApi.infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthApi.extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the DbContext service to the specified IServiceCollection.
    /// </summary>
    /// <param name="serviceCollection">The collection of services to add the DbContext to.</param>
    /// <param name="configuration">The IConfiguration instance</param>
    public static void AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("localDb")));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static void AddIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<IdentityUser, IdentityRole>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static void AddSwaggerGenRegistry(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen();
    }
}