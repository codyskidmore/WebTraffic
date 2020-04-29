using cd.Domain.WebTraffic.Interfaces;
using cd.Domain.WebTraffic.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace cd.Application.Iis
{
    internal class IisLogFileProcessor: ILogFileProcessor
    {
        private readonly ILogger<IisLogFileProcessor> _logger;
        private readonly ILogImportConfiguration _logImportConfiguration;

        public IisLogFileProcessor(ILogger<IisLogFileProcessor> logger, ILogImportConfiguration logImportConfiguration)
		{
			_logger = logger;
            _logImportConfiguration = logImportConfiguration;
		}

		public IEnumerable<StagedIisLogEntry> ImportLogFileIntoEntities(string fileNameAndPath, string hostName)
        {
			List<StagedIisLogEntry> entities = new List<StagedIisLogEntry>();
			try
			{
				_logger.LogInformation($"Entering ImportLogFileIntoEntities. File: {fileNameAndPath}");
				var stopwatch = Stopwatch.StartNew();
				stopwatch.Start();
				List<string> lines = File.ReadAllLines(fileNameAndPath).ToList();

				lines.RemoveAll(l => l.StartsWith("#Software"));
				lines.RemoveAll(l => l.StartsWith("#Version"));
				lines.RemoveAll(l => l.StartsWith("#Date"));

				var fieldHeaderCount = lines.Count(l => l.StartsWith("#Fields"));

				_logger.LogInformation($"Importing {lines.Count() - fieldHeaderCount} lines from {fileNameAndPath}");

				string[] fields = null;

				foreach (string line in lines)
				{
					if (line.StartsWith("#Fields"))
					{
						fields = line.TrimStart('#', 'F', 'i', 'e', 'l', 'd', 's', ':').Trim().Split(' ');
						continue;
					}

					string[] values = line.Split(' ');
					var lineValuesByProperty = GetValueMapFromLine(fields, values);

					var entity = new StagedIisLogEntry();

					foreach (KeyValuePair<string, string> pair in lineValuesByProperty)
					{
                        _logImportConfiguration.PropertyMap[pair.Key].SetValue(entity, pair.Value);
					}

					entity.CsHost = hostName;
					entity.LogFileAndPath = fileNameAndPath;
					entities.Add(entity);
				}

				stopwatch.Stop();
				_logger.LogInformation($"Elapse Time importing {fileNameAndPath}: {stopwatch.Elapsed.Seconds} seconds, {stopwatch.Elapsed.Milliseconds} milliseconds.");
				_logger.LogInformation($"Exiting ImportLogFileIntoEntities. File: {fileNameAndPath}");
				return entities;
			}
			catch (IOException)
			{
				// This happens when the web server locks the file during a write process.
				_logger.LogError($"Could not read log file {fileNameAndPath} because it is being used by another process.");
			}

			return entities;
		}

		private Dictionary<string, string> GetValueMapFromLine(string[] fields, string[] values)
		{
			if (fields.Length != values.Length)
			{
				throw new ArgumentException("fields/values mismatch.");
			}

			var result = new Dictionary<string, string>();

			for (int i = 0; i < fields.Length; i++)
			{
				result.Add(_logImportConfiguration.FieldMap[fields[i]], values[i]);
			}

			return result;
		}
	}
}
