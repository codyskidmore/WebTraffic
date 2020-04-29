using cd.Domain.WebTraffic.Models;
using System.Collections.Generic;

namespace cd.Domain.WebTraffic.Interfaces
{
    public interface ILogFileProcessor
    {
        IEnumerable<StagedIisLogEntry> ImportLogFileIntoEntities(string fileNameAndPath, string hostName);
    }
}
