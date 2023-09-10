using PostSync.Core.Helpers.Configs;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();
var appsettingConfig = configuration.GetSection("Configs");
builder.Services.Configure<AppConfig>(appsettingConfig);

var app = builder.Build();

app.Run();