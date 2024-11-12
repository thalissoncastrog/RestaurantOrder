using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        string secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "",
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

var config = builder.Configuration;

config.AddJsonFile("appsettings.json", true, true).AddJsonFile("ocelot.json");

var sv = builder.Services;
sv.AddOcelot(config);

var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Requisição recebida: {context.Request.Method} {context.Request.Path}");
    await next();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseOcelot().Wait();

app.MapGet("/", () =>
{
    return "API GATEWAY RUNNING";
});

app.Run();