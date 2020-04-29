using cd.Domain.WebTraffic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace cd.Application.Iis
{
    public static class ApplicationExtensionMethods
    {
        public static IServiceCollection AddIisApplication(this IServiceCollection serviceCollection, 
            IConfigurationRoot configurationRoot, int startAtDay)
        {
            serviceCollection.AddTransient<IIisLogService, IisLogService>();
            serviceCollection.AddTransient<ILogFileProcessor, IisLogFileProcessor>();
            serviceCollection.AddSingleton<ILogImportConfiguration>(new LogImportConfiguration(configurationRoot, startAtDay));

            return serviceCollection;
        }
    }
}