using System;
using System.Collections.Generic;
using cd.Infrastructure.ExtensionMethods;

namespace cd.Domain.WebTraffic.Models
{
    public class IisLogEntry
    {
        public int Id { get; set; }
        public DateTime DateAccessed { get; set; }
        public string HttpMethod { get; set; }
        public string UriStem { get; set; }
        public string UriQuery { get; set; }
        public int Port { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Cookie { get; set; }
        public string Referer { get; set; }
        public string Host { get; set; }
        public int Status { get; set; }
        public StagedIisLogEntry StagedIisLogEntry { get; set; }
        public IisLogFile IisLogFile { get; set; }
        public IisSite IisSite { get; set; }

        public static IisLogEntry FromEntity(StagedIisLogEntry s)
        {
            return new IisLogEntry
            {
                Cookie = s.CsCookie,
                DateAccessed = DateTime.Parse($"{s.Date} {s.Time}"),
                Host = s.CsHost,
                HttpMethod = s.CsMethod,
                IpAddress = s.CIp,
                Port = int.Parse(s.SPort),
                Referer = s.CsReferer,
                Status = s.ScStatus.ToInteger(),
                UriQuery = s.CsUriQuery,
                UriStem = s.CsUriStem,
                UserAgent = s.CsUserAgent,
                StagedIisLogEntry = s
            };
        }

        public static List<IisLogEntry> FromEntities(List<StagedIisLogEntry> list)
        {
            List<IisLogEntry> result = new List<IisLogEntry>();
            list.ForEach(s => result.Add(FromEntity(s)));

            return result;
        }
    }
}