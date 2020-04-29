using cd.Domain.WebTraffic.Interfaces;
using cd.Domain.WebTraffic.Models;
using cd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cd.Infrastructure.Iis.Data
{
    internal class IisLogRepository : IIisLogRepository
    {
        private readonly IConfigurationRoot _configuration;

        public IisLogRepository(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddLogs(IisSite site, IisLogFile iisLogFile, IEnumerable<IisLogEntry> iisLogEntries, IEnumerable<StagedIisLogEntry> stagedIisLogEntries)
        {
            string connStr = _configuration.GetConnectionString(DatabaseName.IisDb);
            DbContextOptionsBuilder<IisLogDbContext> builder =
                new DbContextOptionsBuilder<IisLogDbContext>()
                    .UseSqlServer(connStr);

            await using (IisLogDbContext ctx = new IisLogDbContext(builder.Options))
            {
                ctx.Attach(site);
                ctx.Entry(site).State = EntityState.Modified;
                site.LogFiles.Add(iisLogFile);
                iisLogFile.StagedLogEntries.AddRange(stagedIisLogEntries);
                site.LogEntries.AddRange(iisLogEntries);

                return ctx.SaveChanges();
            }
        }

        public async Task<IisSite> AddSite(string hostName, int siteId)
        {
            string connStr = _configuration.GetConnectionString(DatabaseName.IisDb);
            DbContextOptionsBuilder<IisLogDbContext> builder =
                new DbContextOptionsBuilder<IisLogDbContext>()
                    .UseSqlServer(connStr);

            IisSite site = new IisSite()
            {
                HostName = hostName,
                IisId = siteId
            };

            await using (IisLogDbContext ctx = new IisLogDbContext(builder.Options))
            {
                ctx.Sites.Add(site);
                ctx.SaveChanges();

                return site;
            }
        }

        public async Task<int> DeleteProcessedLogFile(IisLogFile processedLogFile)
        {
            string connStr = _configuration.GetConnectionString(DatabaseName.IisDb);
            DbContextOptionsBuilder<IisLogDbContext> builder =
                new DbContextOptionsBuilder<IisLogDbContext>()
                    .UseSqlServer(connStr);

            await using (IisLogDbContext ctx = new IisLogDbContext(builder.Options))
            {
                ctx.LogFiles.Remove(processedLogFile);
                return ctx.SaveChanges();
            }
        }

        // Normally I would inject the dbContext into 
        // the repository. In this case I plan to import
        // logs using parallel processing. This makes 
        // injecting a DbContext impractical because 
        // DbContext is not thread-safe. Each parallel
        // process must have its own DbContext instance.
        public async Task<IisSite> GetIisSite(string hostName)
        {
            string connStr = _configuration.GetConnectionString(DatabaseName.IisDb);
            DbContextOptionsBuilder<IisLogDbContext> builder =
                new DbContextOptionsBuilder<IisLogDbContext>()
                    .UseSqlServer(connStr);

            await using (IisLogDbContext ctx = new IisLogDbContext(builder.Options))
            {
                return ctx.Sites.Include(s => s.LogEntries)
                    .Include(s => s.LogFiles)
                    .SingleOrDefault(s => s.HostName == hostName);
            }
        }

        public async Task<IisLogFile> GetProcessedLogFile(string fileNameAndPath)
        {
            string connStr = _configuration.GetConnectionString(DatabaseName.IisDb);
            DbContextOptionsBuilder<IisLogDbContext> builder =
                new DbContextOptionsBuilder<IisLogDbContext>()
                    .UseSqlServer(connStr);

            await using (IisLogDbContext ctx = new IisLogDbContext(builder.Options))
            {
                return ctx.LogFiles
                    .FirstOrDefault(l => l.LogFileAndPath == fileNameAndPath);
            }
        }
    }
}
