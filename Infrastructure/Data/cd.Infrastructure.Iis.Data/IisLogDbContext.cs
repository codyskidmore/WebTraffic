using cd.Domain.WebTraffic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace cd.Infrastructure.Iis.Data
{
	public class IisLogDbContext : DbContext
	{
		public virtual DbSet<IisSite> Sites { get; set; }
		public virtual DbSet<IisLogEntry> LogEntries { get; set; }
		public virtual DbSet<StagedIisLogEntry> StagedLogEntries { get; set; }
		public virtual DbSet<IisLogFile> LogFiles { get; set; }

		public IisLogDbContext(DbContextOptions<IisLogDbContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.EnableSensitiveDataLogging();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("iis");

			modelBuilder.Entity<IisSite>(entity =>
			{
				entity.ToTable("Sites");
				entity.HasIndex(i => new { Name = i.HostName }).IsUnique();
			});

			modelBuilder.Entity<IisLogFile>(entity =>
			{
				entity.ToTable("LogFiles");
				entity.HasIndex(i => new { i.HostName });
				entity.HasIndex(i => new { i.FileDate });
				entity.HasIndex(i => new { NameAndPath = i.LogFileAndPath }).IsUnique();

				entity.Ignore(lf => lf.LogEntries);
				entity.Ignore(lf => lf.StagedLogEntries);

				entity.HasMany(lf => lf.LogEntries).WithOne(le => le.IisLogFile).OnDelete(DeleteBehavior.Cascade);
				entity.HasMany(lf => lf.StagedLogEntries).WithOne(s => s.IisLogFile).OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<StagedIisLogEntry>(entity =>
			{
				entity.ToTable("StagedLogEntries");

				entity.HasIndex(i => new { i.CsHost, i.Date, i.Time });
				entity.HasIndex(i => new { i.LogFileAndPath });

				entity.Property(e => e.Date)
					.HasColumnName("Date")
					.HasMaxLength(10);

				entity.Property(e => e.Time)
					.HasColumnName("Time")
					.HasMaxLength(8);

				entity.Property(e => e.SSiteName)
					.HasColumnName("s-sitename")
					.HasMaxLength(48);

				entity.Property(e => e.SComputerName)
					.HasColumnName("s-computername")
					.HasMaxLength(48);

				entity.Property(e => e.SIp)
					.HasColumnName("s-ip")
					.HasMaxLength(48);

				entity.Property(e => e.CsMethod)
					.HasColumnName("cs-method")
					.HasMaxLength(8);

				entity.Property(e => e.CsUriStem)
					.HasColumnName("cs-uri-stem")
					.HasMaxLength(255);

				entity.Property(e => e.CsUriQuery)
					.HasColumnName("cs-uri-query")
					.HasMaxLength(2048);

				entity.Property(e => e.SPort)
					.HasColumnName("s-port")
					.HasMaxLength(4);

				entity.Property(e => e.CsUsername)
					.HasColumnName("cs-username")
					.HasMaxLength(256);

				entity.Property(e => e.CIp)
					.HasColumnName("c-ip")
					.HasMaxLength(48);

				entity.Property(e => e.CsVersion)
					.HasColumnName("cs-version")
					.HasMaxLength(48);

				entity.Property(e => e.CsUserAgent)
					.HasColumnName("cs(User-Agent)")
					.HasMaxLength(1024);

				entity.Property(e => e.CsCookie)
					.HasColumnName("cs(Cookie)")
					.HasMaxLength(1024);

				entity.Property(e => e.CsReferer)
					.HasColumnName("cs(Referer)")
					.HasMaxLength(4096);

				entity.Property(e => e.CsHost)
					.HasColumnName("cs-host")
					.HasMaxLength(48);

				entity.Property(e => e.ScStatus)
					.HasColumnName("sc-STATUS");

				entity.Property(e => e.ScSubStatus)
					.HasColumnName("sc-substatus");

				entity.Property(e => e.ScWin32Status)
					.HasColumnName("sc-win32-STATUS");

				entity.Property(e => e.ScByptes)
					.HasColumnName("sc-bytes")
					.HasMaxLength(48);

				entity.Property(e => e.CsBytes)
					.HasColumnName("cs-bytes")
					.HasMaxLength(48);

				entity.Property(e => e.TimeTaken)
					.HasColumnName("time-taken");

				entity.Property(e => e.LogFileAndPath)
					.HasMaxLength(2048);
			});

			base.OnModelCreating(modelBuilder);
		}
	}

	public class IisLogDbContextFactory : IDesignTimeDbContextFactory<IisLogDbContext>
	{
        public IisLogDbContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(System.IO.Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.Development.json")
				.Build();

			var builder = new DbContextOptionsBuilder<IisLogDbContext>();

			var connectionString = configuration.GetConnectionString("IisLogDb");
			builder.UseSqlServer(connectionString);

			return new IisLogDbContext(builder.Options);
		}
	}
}