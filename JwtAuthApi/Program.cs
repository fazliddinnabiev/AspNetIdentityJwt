using HealthChecks.UI.Client;
using JwtAuthApi.extensions;
using JwtAuthApi.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Options = HealthChecks.UI.Configuration.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddUserManager();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddSwaggerGenRegistry();
builder.Services.AddUserDefinedServices();
builder.Services.AddApplicationHealthChecks(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.MapHealthChecksUI(delegate (Options options)
{
    options.UIPath = "/healthcheck-ui";
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();