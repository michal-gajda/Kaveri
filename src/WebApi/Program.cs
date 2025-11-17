namespace Kaveri.WebApi;

using Microsoft.EntityFrameworkCore;
using TickerQ.Caching.StackExchangeRedis.DependencyInjection;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DependencyInjection;
using TickerQ.Instrumentation.OpenTelemetry;
using TickerQ.Utilities.Enums;

public class Program
{
    private const int EXIT_SUCCESS = 0;

    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHealthChecks();

        builder.Services.AddDbContext<KaveriDbContext>(options =>
        {
            options.UseInMemoryDatabase(string.Empty);
        });

        builder.Services.AddTickerQ(options =>
        {
            options.AddStackExchangeRedis(cfg => { cfg.Configuration = "localhost:6379"; });
            options.AddOperationalStore();
            options.AddOpenTelemetryInstrumentation();
            options.AddDashboard(cfg =>
            {
                cfg.SetBasePath("/tickerq");
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseHealthChecks("/health");

        app.UseTickerQ(TickerQStartMode.Immediate);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();

        return EXIT_SUCCESS;
    }
}
