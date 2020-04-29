using cd.Domain.WebTraffic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace cd.Infrastructure.Iis.Data
{
    public static class IisLogDataExtensionMethods
    {
        public static IServiceCollection AddIisLogData(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IIisLogRepository, IisLogRepository>();
            return serviceCollection;
        }
    }
}