using Microsoft.Extensions.Options;
using PostSync.API.Extensions;
using PostSync.Core.Helpers.Configs;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();
var appsettingConfig = configuration.GetSection("Configs");
builder.Services.Configure<IntegrationAppConfig>(appsettingConfig);

var appConfig = builder.Services.BuildServiceProvider().GetService<IOptions<IntegrationAppConfig>>().Value;

builder.Services.AddServices();

builder.Services.AddMassTransitConfigs(appConfig.RabbitMq.Host, appConfig.RabbitMq.Username,
    appConfig.RabbitMq.Password);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();