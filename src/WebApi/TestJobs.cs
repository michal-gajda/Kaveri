using TickerQ.Utilities.Base;

namespace Kaveri.WebApi;

public class TestJobs
{
    [TickerFunction("TestJob")]
    public Task TestJob(TickerFunctionContext context, CancellationToken cancellationToken)
    {
        Console.WriteLine($"TickerQ is working! Job ID: {context.Id}");
        return Task.CompletedTask;
    }

    [TickerFunction("TestJobException")]
    public async Task TestJob2(TickerFunctionContext context, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        throw new Exception("Test exception");
    }
}
