using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Data;

namespace DataMigrationTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<DataMigrator>>();
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            var passwordSettings = serviceProvider.GetRequiredService<IOptions<PasswordSettings>>().Value;
            var passwordHash = passwordSettings.PasswordHash;

            var dataMigrator = new DataMigrator(logger, dbContext, passwordHash);

            Console.WriteLine("Enter the path to the JSON file:");
            string jsonFilePath = Console.ReadLine();
            Console.WriteLine("Enter the path to the XML file:");
            string xmlFilePath = Console.ReadLine();

            await dataMigrator.ExecuteMigration(jsonFilePath, xmlFilePath);
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();
            services.Configure<PasswordSettings>(configuration.GetSection("PasswordSettings"));
            services.AddSingleton(configuration);
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddLogging(configure => configure.AddConsole());

            return services;
        }
    }
}
