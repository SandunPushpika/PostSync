using System.Text;
using Integration.Core.Domains.DTOs;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PostSync.Core.Helpers.Configs;
using PostSync.Core.Interfaces;
using PostSync.Core.Services;
using PostSync.Infrastructure.Services;

namespace PostSync.API.Extensions;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<ResponseService>();

        collection.AddScoped<IDbContext, DbContext>();
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IAuthService, AuthService>();

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

    public static IServiceCollection AddMassTransitConfigs(this IServiceCollection collection, string host, string username, string password)
    {
        collection.AddMassTransit(conf =>
        {
            conf.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(host,"/", config =>
                {
                    config.Username(username);
                    config.Password(password);
                });
                
                cfg.Message<UserModel>(m => m.SetEntityName("UserModel"));
                
            });
        });

        return collection;
    }
    
}