using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PostSync.Core.Helpers.Configs;
using PostSync.Core.Interfaces;
using PostSync.Core.Services;
using PostSync.Core.Services.Integrations;
using PostSync.Infrastructure.Services;
using PostSync.Infrastructure.Services.Integrations;

namespace PostSync.API.Extensions;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<ResponseService>();

        collection.AddScoped<IDbContext, DbContext>();
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IAuthService, AuthService>();
        collection.AddScoped<IHttpClientHelper, HttpClientHelper>();
        collection.AddScoped<IFacebookService, FacebookService>();
        collection.AddScoped<IHttpContextService, HttpContextService>();
        collection.AddScoped<IIntegrationService, IntegrationService>();
        collection.AddHttpContextAccessor();
        
        return collection;
    }

    public static AuthenticationBuilder AddCustomAuthentication(this IServiceCollection collection, AppConfig config)
    {
        return collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Jwt.Key)),
                    ValidIssuer = config.Jwt.Issuer,
                    ValidAudience = config.Jwt.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
    }

    public static IServiceCollection AddCustomCorsPolicy(this IServiceCollection collection)
    {
        collection.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyMethod();
            });
        });

        return collection;
    }
    
}