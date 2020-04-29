using cd.Domain.WebTraffic.Interfaces;
using cd.Domain.WebTraffic.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using cd.Domain.Infrastructure;

namespace cd.Application.Iis
{
	public class IisLogService : IIisLogService
	{
		private readonly IIisLogRepository _repo;
        private readonly ILogFileProvider _logFileProvider;
        private readonly ILogFileProcessor _logFileProcessor;
		private readonly List<SiteInfo> _sites;
		private readonly ILogger<IisLogService> _logger;

        public IisLogService(IIisLogRepository repo, ILogFileProvider logFileProvider, ILogFileProcessor logFileProcessor,
			List<SiteInfo> sites, ILogger<IisLogService> logger)
		{
			_repo = repo;
			_logFileProvider = logFileProvider;
			_logFileProcessor = logFileProcessor;
			_sites = sites;
			_logger = logger;
        }

        public async Task ImportIisLogFiles()
		{
			_logger.LogInformation("Entering ImportIisLogFiles");

            await Task.WhenAll(_sites.AsParallel().Select(async site => await ProcessSiteLogs(site)));

            _logger.LogInformation("Exiting ImportIisLogFiles");
		}

        private async Task ProcessSiteLogs(SiteInfo iisSite)
        {
            IisSite site = await _repo.GetIisSite(iisSite.HostName);
            if (site == null)
            {
                _logger.LogInformation($"Added site {iisSite.HostName}, Site Id:{iisSite.SiteId}.");
                site = await _repo.AddSite(iisSite.HostName, iisSite.SiteId);
            }

            string[] logFiles = _logFileProvider.GetLogFiles(iisSite.SiteId, iisSite.LogFileAndPath);

            DateTime processingDate = DateTime.Now;

            var logFileStopwatch = Stopwatch.StartNew();
            var taskStopwatch = Stopwatch.StartNew();

            _logger.LogInformation("Entering file processing loop");

            foreach (string fileNameAndPath in logFiles)
            {
                await ProcessLogFiles(iisSite, logFileStopwatch, fileNameAndPath, site, taskStopwatch, processingDate);
            }

            _logger.LogInformation("Exiting file processing loop");
        }

        private async Task ProcessLogFiles(SiteInfo iisSite, Stopwatch logFileStopwatch, string fileNameAndPath, IisSite site,
            Stopwatch taskStopwatch, DateTime processingDate)
        {
            logFileStopwatch.Start();
            var fileInfo = new FileInfo(fileNameAndPath);

            var processedLogFile = await _repo.GetProcessedLogFile(fileNameAndPath);

            if (processedLogFile?.DateImported < fileInfo.LastWriteTime && processedLogFile.Size != fileInfo.Length)
            {
                _logger.LogInformation($"Reprocessing log file:{fileNameAndPath}");
                await _repo.DeleteProcessedLogFile(processedLogFile);
            }

            if (site.LogFiles.Exists(f => f.LogFileAndPath.Matches(fileNameAndPath)))
            {
                _logger.LogInformation($"Skipping log file:{fileNameAndPath}");
                return;
            }

            _logger.LogInformation($"Importing StagedLogEntities.");
            List<StagedIisLogEntry> stagedLogEntries =
                _logFileProcessor.ImportLogFileIntoEntities(fileNameAndPath, iisSite.HostName).ToList();
            _logger.LogInformation($"Converting StagedLogEntities to LogEntries.");

            taskStopwatch.Start();
            List<IisLogEntry> logEntries = await Task.FromResult(IisLogEntry.FromEntities(stagedLogEntries));
            taskStopwatch.Stop();
            _logger.LogInformation(
                $"Elapse time Converting StagedLogEntities: {taskStopwatch.Elapsed.Seconds} seconds, {taskStopwatch.Elapsed.Milliseconds} milliseconds.");

            IisLogFile logFile = new IisLogFile(fileInfo, iisSite, stagedLogEntries,
                logEntries, site, processingDate, fileNameAndPath);

            _logger.LogInformation("Saving log results");

            await _repo.AddLogs(site, logFile, logEntries, stagedLogEntries);

            logFileStopwatch.Stop();

            _logger.LogInformation(
                $"Elapse time processing {fileNameAndPath}: {logFileStopwatch.Elapsed.Seconds} seconds, {logFileStopwatch.Elapsed.Milliseconds} milliseconds.");
        }
    }
}
