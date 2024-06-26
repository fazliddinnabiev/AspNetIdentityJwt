﻿using System.Reflection;
using System.Text;
using HealthChecks.SqlServer;
using JwtAuthApi.core.Interfaces;
using JwtAuthApi.HealthChecks;
using JwtAuthApi.infrastructure;
using JwtAuthApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
    /// <param name="configuration">The IConfiguration instance.</param>
    public static void AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("authDb")));
    }

    /// <summary>
    /// Registers <see cref="IdentityUser"/> and <see cref="IdentityRole"/>.
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
    /// <param name="configuration">An instance of type <see cref="IConfiguration"/>.</param>
    /// <exception cref="ArgumentNullException">An exception is thrown if the token secret is not provided.</exception>
    public static void AddAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
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
                    ValidateAudience = false,
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
            var xmlFilePath = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilePath));

            options.SwaggerDoc(name: "v1", info: new OpenApiInfo
            {
                Title = "Authentication and authorization",
                Version = "V1",
                Description = "ASP.NET web api",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Email = "test@test.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });

            options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });
    }

    /// <summary>
    /// Registers services user defined services.
    /// </summary>
    /// <param name="serviceCollection">An instance of type <see cref="IServiceCollection"/>.</param>
    public static void AddUserDefinedServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthService, AuthService>();
    }

    /// <summary>
    /// Register health check services.
    /// </summary>
    /// <param name="serviceCollection">An instance of type <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The IConfiguration instance.</param>
    /// <exception cref="InvalidOperationException">An exception is thrown if the connection string is not provided.</exception>
    public static void AddApplicationHealthChecks(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("authDb");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Could not find a connection string named 'authDb'.");
        }

        serviceCollection.AddHealthChecksUI(setupSettings: options =>
        {
            options.SetEvaluationTimeInSeconds(seconds: 10);
            options.MaximumHistoryEntriesPerEndpoint(maxValue: 20);
            options.SetApiMaxActiveRequests(10);
            options.AddHealthCheckEndpoint(name: "Jwt auth api", uri: "/health");
            options.SetMinimumSecondsBetweenFailureNotifications(60);
        }).AddInMemoryStorage();

        serviceCollection.AddHealthChecks().AddSqlServer(new SqlServerHealthCheckOptions
        {
            ConnectionString = connectionString,
            CommandText = "select 1"
        }, failureStatus: HealthStatus.Unhealthy, name: "authDb", tags: new[] { "Database" });

        var cDriveHealthCheck = new HealthCheckRegistration(
            "Health of C",
            new DriveHealthCheck(driveName: "C", healthyThresholdGb: 200, unhealthyThresholdGb: 100),
            HealthStatus.Unhealthy,
            new[] { "Drive" });

        var dDriveHealthCheck = new HealthCheckRegistration(
            "Health of C strict",
            new DriveHealthCheck(driveName: "C", healthyThresholdGb: 400, unhealthyThresholdGb: 300),
            HealthStatus.Unhealthy,
            new[] { "Drive" });

        serviceCollection.AddHealthChecks()
            .Add(cDriveHealthCheck)
            .Add(dDriveHealthCheck);
    }
}