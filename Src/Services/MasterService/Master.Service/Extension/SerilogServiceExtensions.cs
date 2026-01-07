namespace Master.Service.Extension;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.File;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class SerilogServiceExtensions
{
    public static void AddSerilogInfo(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/master/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        services.AddSingleton(Log.Logger);
    }

    public static IHostBuilder UseSerilogInfo(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        return hostBuilder.UseSerilog((hostingContext, loggerConfiguration) =>
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/master/log-.txt", rollingInterval: RollingInterval.Day)
        );
    }
}
