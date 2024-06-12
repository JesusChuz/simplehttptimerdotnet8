using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    }).ConfigureLogging(log =>
    {
        log.Services.Configure<LoggerFilterOptions>(opt =>
        {
            LoggerFilterRule newRule = opt.Rules.FirstOrDefault(rule => rule.ProviderName == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");
            if (newRule is not null)
            {
                opt.Rules.Remove(newRule);
            }
        });
    })
    .Build();

host.Run();