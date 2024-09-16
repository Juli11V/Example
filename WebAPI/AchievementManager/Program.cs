using AchievementManager.Kafka;
using AchievementManager.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Domain.Constants;

namespace AchievementManager;

class Program
{
    static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var path = GetConfigurationPath(Directory.GetCurrentDirectory());
                config.AddJsonFile(path, optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((context, collection) =>
            {
                collection.Configure<KafkaOptions>(context.Configuration.GetSection(KafkaOptions.Kafka));
                collection.Configure<MongoDbOptions>(context.Configuration.GetSection(MongoDbOptions.MongoDb));
                collection.AddHostedService<KafkaConsumer>();
                collection.AddScoped<IAchievementService, AchievementService>();
            });

    private static string GetConfigurationPath(string fullPath)
    {
        const string directoryToStopAt = "bin";
        const string config = "appsettings.json";

        var directoryInfo = new DirectoryInfo(fullPath);

        while (directoryInfo.Name != directoryToStopAt && directoryInfo.Parent != null)
        {
            directoryInfo = directoryInfo.Parent;
        }

        if (directoryInfo.Name != directoryToStopAt)
        {
            return fullPath;
        }
        
        return $"{directoryInfo.Parent?.FullName}/{config}";
    }
}