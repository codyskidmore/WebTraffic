using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cd.Domain.WebTraffic.Interfaces
{
    public interface ILogFileProvider
    {
        string[] GetLogFiles(int siteId, string logFileAndPath);
    }
}
