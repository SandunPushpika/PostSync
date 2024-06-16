using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PostSync.Core.Helpers.Configs;
using PostSync.Core.Interfaces;
using PostSync.Core.Services;
using PostSync.Core.Services.Integrations;
using PostSync.Infrastructure.Services;
using PostSync.Infrastructure.Services.BackgroundJobs;
using PostSync.Infrastructure.Services.Integrations;
using Quartz;

namespace PostSync.API.Extensions;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        collection.AddScoped<IDbContext, DbContext>();
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IAuthService, AuthService>();
        collection.AddScoped<IHttpClientHelper, HttpClientHelper>();
        collection.AddScoped<IFbOauthService, FbOauthService>();
        collection.AddScoped<IHttpContextService, HttpContextService>();
        collection.AddScoped<IIntegrationService, IntegrationService>();
        collection.AddScoped<IPageSessionService, PageSessionService>();
        collection.AddScoped<IPostDbService, PostDbService>();
        collection.AddScoped<ResponseService>();
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

    public static IServiceCollection AddSwaggerOptions(this IServiceCollection collection)
    {
        collection.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "JWT Token",
                Description = "Enter the token",
                Scheme = "Bearer",
                Type = SecuritySchemeType.Http
        
            });
    
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        return collection;
    }

    public static IServiceCollection AddQuartzConfig(this IServiceCollection collection, string databaseConnection)
    {

        collection.AddQuartz(q =>
        {
            q.SchedulerId = "qtSched";
            q.SchedulerName = "PostSync Scheduler Config";
            
            q.UsePersistentStore(c =>
            {
                c.UsePostgres(con =>
                {
                    con.ConnectionString = databaseConnection;
                    con.TablePrefix = "QRTZ_";
                });
                c.UseJsonSerializer();
                c.UseProperties = true;
            });
            
            var jobKey = new JobKey("test2");
            q.AddJob<TestHello>(jobKey);
            
            q.AddTrigger(opt => opt.ForJob(jobKey)
                .WithIdentity("HelloWorldJob-trigger")
                .WithCronSchedule("0/5 * * * * ?"));
            
        });

        collection.AddQuartzHostedService( q => q.WaitForJobsToComplete = true);
        
        return collection;
    }
    
}