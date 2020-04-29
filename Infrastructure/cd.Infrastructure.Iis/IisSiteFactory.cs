using System.Collections.Generic;
using Microsoft.Web.Administration;
using cd.Domain.WebTraffic.Models;

namespace cd.Infrastructure.Iis
{
    public class IisSiteFactory
    {
        public static List<SiteInfo> GetIisSites()
        {
            ServerManager server = new ServerManager();
            SiteCollection sites = server.Sites;

            List<SiteInfo> result = new List<SiteInfo>();

            foreach (var site in sites)
            {
                result.Add(FromIisSiteEntity(site));
            }

            return result;
        }

        private static SiteInfo FromIisSiteEntity(Site site)
        {
            return new SiteInfo
            {
                LogFileAndPath = site.LogFile.Directory,
                HostName = site.Name,
                SiteId = (int)site.Id
            };
        }

        //public static List<SiteInfo> GetTestSites()
        //{
        //    return new List<SiteInfo>
        //    {
        //        new SiteInfo { HostName = "linkbin.in", LogFileAndPath = @"C:\temp\Logs", SiteId = 2}
        //    };
        //}
    }
}
