namespace cd.Domain.WebTraffic.Models
{
    public class StagedIisLogEntry
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string SSiteName { get; set; }
        public string SComputerName { get; set; }
        public string SIp { get; set; }
        public string CsMethod { get; set; }
        public string CsUriStem { get; set; }
        public string CsUriQuery { get; set; }
        public string SPort { get; set; }
        public string CsUsername { get; set; }
        public string CIp { get; set; }
        public string CsVersion { get; set; }
        public string CsUserAgent { get; set; }
        public string CsCookie { get; set; }
        public string CsReferer { get; set; }
        public string CsHost { get; set; }
        public string ScStatus { get; set; }
        public string ScSubStatus { get; set; }
        public string ScWin32Status { get; set; }
        public string ScByptes { get; set; }
        public string CsBytes { get; set; }
        public string TimeTaken { get; set; }
        public string LogFileAndPath { get; set; }
        public IisLogFile IisLogFile { get; set; }
    }
}