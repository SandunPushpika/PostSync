using Integration.Core.Domains.DTOs;
using Integration.Core.Interfaces;
using Integration.Core.Interfaces.OAuth;
using Integration.Infrastructure.Services;
using Integration.Infrastructure.Services.Consumers;
using Integration.Infrastructure.Services.OAuth;
using MassTransit;

namespace PostSync.API.Extensions;

public static class Extensions
{

    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        collection.AddScoped<IHttpHelper, HttpHelper>();
        collection.AddScoped<IFacebookOAuth, FacebookOAuth>();
        
        return collection;
    }
    
    public static IServiceCollection AddMassTransitConfigs(this IServiceCollection collection, string host, string username, string password)
    {
        collection.AddMassTransit(conf =>
        {
            
            conf.AddConsumer<CommonConsumer>();
            
            conf.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(host,"/", config =>
                {
                    config.Username(username);
                    config.Password(password);
                });
                
                cfg.ReceiveEndpoint("integration-authorize", e =>
                {
                    e.Lazy = true;
                    e.UseRawJsonSerializer();
                    e.Consumer<CommonConsumer>();
                });
            });
        });

        return collection;
    }
    
}