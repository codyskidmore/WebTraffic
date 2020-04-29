using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace cd.Domain.WebTraffic.Interfaces
{
    public interface ILogImportConfiguration
    {
        Dictionary<string, string> FieldMap { get; }
        Dictionary<string, PropertyInfo> PropertyMap { get; }
        /// <summary>
        /// The number of days from today's date back in
        /// time to import files from. 
        /// 
        /// Example: StartAtDay = 5 .. import all logs
        /// from for the last five days including today.
        /// </summary>
        int StartAtDay { get; }
    }
}
