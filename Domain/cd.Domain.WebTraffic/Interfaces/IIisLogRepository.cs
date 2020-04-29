using cd.Domain.WebTraffic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cd.Domain.WebTraffic.Interfaces
{
    public interface IIisLogRepository
    {
        Task<IisSite> GetIisSite(string hostName);
        Task<IisSite> AddSite(string hostName, int siteId);
        Task<IisLogFile> GetProcessedLogFile(string fileNameAndPath);
        Task<int> DeleteProcessedLogFile(IisLogFile iisLogFile);
        Task<int> AddLogs(IisSite site, IisLogFile iisLogFile, IEnumerable<IisLogEntry> iisLogEntries, 
            IEnumerable<StagedIisLogEntry> stagedIisLogEntries);
    }
}
