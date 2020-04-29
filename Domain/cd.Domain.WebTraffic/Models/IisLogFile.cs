using System;
using System.Collections.Generic;
using System.IO;

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

        public IisLogFile(FileInfo fileInfo, SiteInfo siteInfo, List<StagedIisLogEntry> 
            stagedLogEntries, IEnumerable<IisLogEntry> logEntries, IisSite iisSite, DateTime processingDate, 
            string logFileAndPath)
        {
            Size = fileInfo.Length;
            FileDate = fileInfo.LastWriteTime;
            DateImported = processingDate;
            LogFileAndPath = logFileAndPath;
            HostName = siteInfo.HostName;
            StagedLogEntries = new List<StagedIisLogEntry>();
            LogEntries = new List<IisLogEntry>();
            //StagedLogEntries = (List<StagedIisLogEntry>)stagedLogEntries;
            //LogEntries = (List<IisLogEntry>)logEntries;
            //IisSite = iisSite;

            //LogEntries.ForEach(le =>
            //{
            //    le.IisLogFile = this;
            //    le.IisSite = iisSite;
            //});

            //stagedLogEntries.ForEach(se => se.IisLogFile = this);
        }
        public IisLogFile() // Required for migrations
        {

        }
    }
}