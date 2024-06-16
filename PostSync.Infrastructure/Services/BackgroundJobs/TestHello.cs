using Quartz;

namespace PostSync.Infrastructure.Services.BackgroundJobs;

[DisallowConcurrentExecution]
public class TestHello : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"Hello world works at {DateTime.Now.ToString()}");
        return Task.CompletedTask;
    }
}