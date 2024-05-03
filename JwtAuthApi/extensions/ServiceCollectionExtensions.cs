using System.Text;
using JwtAuthApi.core.Interfaces;
using JwtAuthApi.core.Services;
using JwtAuthApi.infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace JwtAuthApi.extensions;

/// <summary>
/// Represents extension methods of <see cref="IServiceCollection"/> to inject dependencies.
/// </summary>
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
            options.UseSqlServer(configuration.GetConnectionString("LocalDb")));
    }

    /// <summary>
    /// Registers <see cref="IdentityUser"/> and <see cref="IdentityRole"/>
    /// </summary>
    /// <param name="serviceCollection">An instance of type <see cref="IServiceCollection"/>.</param>
    public static void AddIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }

    /// <summary>
    /// Registers <see cref="UserManager{TUser}"/> <see cref="SignInManager{TUser}"/>, and <see cref="RoleManager{TRole}"/> services.
    /// </summary>
    /// <param name="serviceCollection">An instance of type <see cref="IServiceCollection"/>.</param>
    public static void AddUserManager(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<UserManager<IdentityUser>>();
        serviceCollection.AddScoped<SignInManager<IdentityUser>>();
        serviceCollection.AddScoped<IdentityRole>();
    }

    /// <summary>
    ///  Registers authentication with JWT token.
    /// </summary>
    /// <param name="serviceCollection">An instance of type <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">An instance of type <see cref="IConfiguration"/></param>
    /// <exception cref="ArgumentNullException">An exception is thrown if the token secret is not provided.</exception>
    public static void AddAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthentication().AddJwtBearer(options =>
        {
            string? jwtKey = configuration.GetSection("JWT:Key").Value;
            if (jwtKey is null)
            {
                throw new ArgumentNullException(nameof(jwtKey));
            }

            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidIssuer = configuration.GetSection("JWT:ValidIssuer").Value,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                SaveSigninToken = true
            };
        });
    }

    /// <summary>
    /// Registers and configures swagger for authentication and authorization.
    /// </summary>
    /// <param name="serviceCollection">An instance of type <see cref="IServiceCollection"/>.</param>
    public static void AddSwaggerGenRegistry(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
            });
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static void AddUserDefinedServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthService, AuthService>();
    }
}