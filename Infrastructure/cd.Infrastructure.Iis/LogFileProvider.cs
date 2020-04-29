using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cd.Domain.WebTraffic.Interfaces;
using Microsoft.Extensions.Logging;

namespace cd.Infrastructure.Iis
{
    internal class LogFileProvider : ILogFileProvider
    {
        private readonly ILogger<LogFileProvider> _logger;
        private readonly ILogImportConfiguration _logImportConfiguration;

        public LogFileProvider(ILogger<LogFileProvider> logger, ILogImportConfiguration logImportConfiguration)
        {
            _logger = logger;
            _logImportConfiguration = logImportConfiguration;
        }
        public string[] GetLogFiles(int siteId, string logFileAndPath)
        {
            string logPath = $"{Environment.ExpandEnvironmentVariables(logFileAndPath)}\\W3SVC{siteId}";

            if (!Directory.Exists(logPath))
            {
                var newPath = Environment.ExpandEnvironmentVariables(logFileAndPath);
                _logger.LogInformation($"Directory Does not exist: {logPath}. Resetting path to: {newPath}");
                logPath = newPath;
            }

            string[] logFiles = Directory.GetFiles(logPath, "*.log")
                .Where(x => new FileInfo(x).LastWriteTime.Date >= DateTime.Today.AddDays(_logImportConfiguration.StartAtDay).Date).ToArray();

            _logger.LogInformation($"Selected {logFiles.Length } files for site {siteId}.");

            return logFiles;
        }
    }
}
