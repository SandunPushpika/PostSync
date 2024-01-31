using Dapper;
using Microsoft.Extensions.Options;
using PostSync.API.Extensions;
using PostSync.API.Middlewears;
using PostSync.Core.Helpers.Configs;

var builder = WebApplication.CreateBuilder(args);
DefaultTypeMap.MatchNamesWithUnderscores = true;

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();
var appsettingConfig = configuration.GetSection("Configs");
builder.Services.Configure<AppConfig>(appsettingConfig);

var appConfig = builder.Services.BuildServiceProvider().GetService<IOptions<AppConfig>>().Value;

builder.Services.AddControllers();

builder.Services.AddServices();

builder.Services.AddCustomAuthentication(appConfig);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();