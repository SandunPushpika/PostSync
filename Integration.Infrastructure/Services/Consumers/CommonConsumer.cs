using Integration.Core.Domains.DTOs;
using MassTransit;
using Shared.DTOs;

namespace Integration.Infrastructure.Services.Consumers;

public class CommonConsumer : IConsumer<TestModel>
{
    public async Task Consume(ConsumeContext<TestModel> context)
    {
        Console.WriteLine("Message received");
        Console.WriteLine(context.Message.LastName);
        Console.WriteLine(context.Message.Id);
    }
}