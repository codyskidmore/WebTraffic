using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using cd.Domain.Infrastructure;
using cd.Domain.WebTraffic.Interfaces;
using cd.Domain.WebTraffic.Models;
using Microsoft.Extensions.Configuration;

namespace cd.Application.Iis
{
    internal class LogImportConfiguration : ILogImportConfiguration
    {
        public Dictionary<string, string> FieldMap { get; }
        public Dictionary<string, PropertyInfo> PropertyMap { get; }

        public int StartAtDay { get; }

        public LogImportConfiguration(IConfigurationRoot configurationRoot, int startAtDay)
        {
            var fieldMappingFile = configurationRoot.GetValue<string>("FieldMapping");
            var fullpath = Path.Combine(AppContext.BaseDirectory, fieldMappingFile);
            FieldMap = fullpath.LoadObjectFromJsonFile<Dictionary<string, string>>();
            StartAtDay = startAtDay;
            PropertyMap = GetPropertyMap();
        }

        private Dictionary<string, PropertyInfo> GetPropertyMap()
        {
            var propertyMap = new Dictionary<string, PropertyInfo>();
            var typeValue = typeof(StagedIisLogEntry);
            var stagedProperties = typeValue.GetProperties();

            foreach (PropertyInfo p in stagedProperties)
            {
                propertyMap.Add(p.Name, p);
            }

            return propertyMap;
        }

    }
}
