using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using cd.Application.Iis;
using cd.Domain.WebTraffic.Interfaces;
using cd.Infrastructure.Iis;
using cd.Infrastructure.Iis.Data;
using Microsoft.Extensions.Configuration;

namespace ImportIisLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var serviceProvider = RegisterServices();
            var service = serviceProvider.GetService<IIisLogService>();
            service.ImportIisLogFiles();
        }
        private static IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            IConfigurationRoot configuration = GetConfiguration();

            return services.AddSingleton<IConfigurationRoot>(configuration)
                .AddIisApplication(configuration, configuration.GetValue<int>("StartAtDay"))
                .AddIisLogData()
                .AddIisInfrastructure()
                .AddLogging()
                .BuildServiceProvider();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile(Debugger.IsAttached ? "appsettings.Development.json" : "appsettings.json");

            builder.AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
