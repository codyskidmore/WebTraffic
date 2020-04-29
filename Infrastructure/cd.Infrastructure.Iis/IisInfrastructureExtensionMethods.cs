using System.Collections.Generic;
using cd.Domain.WebTraffic.Interfaces;
using cd.Domain.WebTraffic.Models;
using Microsoft.Extensions.DependencyInjection;

namespace cd.Infrastructure.Iis
{
    public static class IisInfrastructureExtensionMethods
    {
        public static IServiceCollection AddIisInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ILogFileProvider, LogFileProvider>();
            serviceCollection.AddSingleton<List<SiteInfo>>(IisSiteFactory.GetIisSites());
            return serviceCollection;
        }
    }
}