using System.Collections.Generic;

namespace cd.Domain.WebTraffic.Models
{
    public class IisSite
    {
        public int Id { get; set; }
        public int IisId { get; set; }
        public string HostName { get; set; }
        public List<IisLogEntry> LogEntries { get; set; }
        public List<IisLogFile> LogFiles { get; set; }

        public IisSite()
        {
            LogEntries = new List<IisLogEntry>();
            LogFiles = new List<IisLogFile>();
        }
    }
}