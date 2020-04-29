using System;
using System.Collections.Generic;

namespace cd.Domain.WebTraffic.Models
{
    public class IisLogFile
    {
        public int Id { get; set; }
        public string LogFileAndPath { get; set; }
        public long Size { get; set; }
        public DateTime FileDate { get; set; }
        public DateTime DateImported { get; set; }
        public string HostName { get; set; }
        public List<StagedIisLogEntry> StagedLogEntries { get; set; }
        public List<IisLogEntry> LogEntries { get; set; }
        public IisSite IisSite { get; set; }
    }
}