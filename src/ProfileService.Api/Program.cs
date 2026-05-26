using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProfileService.Api.Endpoints;
using ProfileService.Api.OpenApi;
using ProfileService.Api.Security;
using ProfileService.Application.Extensions;
using ProfileService.Infrastructure.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApiConfiguration();
builder.Services.AddCorsConfiguration();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

string issuer = builder.Configuration["JwtSettings:Issuer"]
    ?? throw new InvalidOperationException("JwtSettings:Issuer is missing.");

string audience = builder.Configuration["JwtSettings:Audience"]
    ?? throw new InvalidOperationException("JwtSettings:Audience is missing.");

string signinKey = builder.Configuration["JwtSettings:SigninKey"]
    ?? throw new InvalidOperationException("JwtSettings:SigninKey is missing.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        // regler för vad en giltig token är.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(signinKey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseCors("Frontend");

app.MapOpenApiEndpoints();

app.MapProfileEndpoints();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();







