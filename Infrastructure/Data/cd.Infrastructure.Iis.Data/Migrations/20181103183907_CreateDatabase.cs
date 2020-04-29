using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cd.Infrastructure.Iis.Data
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "iis");

            migrationBuilder.CreateTable(
                name: "Sites",
                schema: "iis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IisId = table.Column<int>(nullable: false),
                    HostName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogFiles",
                schema: "iis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogFileAndPath = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    FileDate = table.Column<DateTime>(nullable: false),
                    DateImported = table.Column<DateTime>(nullable: false),
                    HostName = table.Column<string>(nullable: true),
                    IisSiteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogFiles_Sites_IisSiteId",
                        column: x => x.IisSiteId,
                        principalSchema: "iis",
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StagedLogEntries",
                schema: "iis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<string>(maxLength: 10, nullable: true),
                    Time = table.Column<string>(maxLength: 8, nullable: true),
                    ssitename = table.Column<string>(name: "s-sitename", maxLength: 48, nullable: true),
                    scomputername = table.Column<string>(name: "s-computername", maxLength: 48, nullable: true),
                    sip = table.Column<string>(name: "s-ip", maxLength: 48, nullable: true),
                    csmethod = table.Column<string>(name: "cs-method", maxLength: 8, nullable: true),
                    csuristem = table.Column<string>(name: "cs-uri-stem", maxLength: 255, nullable: true),
                    csuriquery = table.Column<string>(name: "cs-uri-query", maxLength: 2048, nullable: true),
                    sport = table.Column<string>(name: "s-port", maxLength: 4, nullable: true),
                    csusername = table.Column<string>(name: "cs-username", maxLength: 256, nullable: true),
                    cip = table.Column<string>(name: "c-ip", maxLength: 48, nullable: true),
                    csversion = table.Column<string>(name: "cs-version", maxLength: 48, nullable: true),
                    csUserAgent = table.Column<string>(name: "cs(User-Agent)", maxLength: 1024, nullable: true),
                    csCookie = table.Column<string>(name: "cs(Cookie)", maxLength: 48, nullable: true),
                    csReferer = table.Column<string>(name: "cs(Referer)", maxLength: 4096, nullable: true),
                    cshost = table.Column<string>(name: "cs-host", maxLength: 48, nullable: true),
                    scSTATUS = table.Column<string>(name: "sc-STATUS", nullable: true),
                    scsubstatus = table.Column<string>(name: "sc-substatus", nullable: true),
                    scwin32STATUS = table.Column<string>(name: "sc-win32-STATUS", nullable: true),
                    scbytes = table.Column<string>(name: "sc-bytes", maxLength: 48, nullable: true),
                    csbytes = table.Column<string>(name: "cs-bytes", maxLength: 48, nullable: true),
                    timetaken = table.Column<string>(name: "time-taken", nullable: true),
                    LogFileAndPath = table.Column<string>(maxLength: 2048, nullable: true),
                    IisLogFileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagedLogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StagedLogEntries_LogFiles_IisLogFileId",
                        column: x => x.IisLogFileId,
                        principalSchema: "iis",
                        principalTable: "LogFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                schema: "iis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateAccessed = table.Column<DateTime>(nullable: false),
                    HttpMethod = table.Column<string>(nullable: true),
                    UriStem = table.Column<string>(nullable: true),
                    UriQuery = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(nullable: true),
                    UserAgent = table.Column<string>(nullable: true),
                    Cookie = table.Column<string>(nullable: true),
                    Referer = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StagedIisLogEntryId = table.Column<int>(nullable: true),
                    IisLogFileId = table.Column<int>(nullable: true),
                    IisSiteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogEntries_LogFiles_IisLogFileId",
                        column: x => x.IisLogFileId,
                        principalSchema: "iis",
                        principalTable: "LogFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogEntries_Sites_IisSiteId",
                        column: x => x.IisSiteId,
                        principalSchema: "iis",
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogEntries_StagedLogEntries_StagedIisLogEntryId",
                        column: x => x.StagedIisLogEntryId,
                        principalSchema: "iis",
                        principalTable: "StagedLogEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_IisLogFileId",
                schema: "iis",
                table: "LogEntries",
                column: "IisLogFileId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_IisSiteId",
                schema: "iis",
                table: "LogEntries",
                column: "IisSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_StagedIisLogEntryId",
                schema: "iis",
                table: "LogEntries",
                column: "StagedIisLogEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_LogFiles_FileDate",
                schema: "iis",
                table: "LogFiles",
                column: "FileDate");

            migrationBuilder.CreateIndex(
                name: "IX_LogFiles_HostName",
                schema: "iis",
                table: "LogFiles",
                column: "HostName");

            migrationBuilder.CreateIndex(
                name: "IX_LogFiles_IisSiteId",
                schema: "iis",
                table: "LogFiles",
                column: "IisSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_LogFiles_LogFileAndPath",
                schema: "iis",
                table: "LogFiles",
                column: "LogFileAndPath",
                unique: true,
                filter: "[LogFileAndPath] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_HostName",
                schema: "iis",
                table: "Sites",
                column: "HostName",
                unique: true,
                filter: "[HostName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StagedLogEntries_IisLogFileId",
                schema: "iis",
                table: "StagedLogEntries",
                column: "IisLogFileId");

            migrationBuilder.CreateIndex(
                name: "IX_StagedLogEntries_LogFileAndPath",
                schema: "iis",
                table: "StagedLogEntries",
                column: "LogFileAndPath");

            migrationBuilder.CreateIndex(
                name: "IX_StagedLogEntries_cs-host_Date_Time",
                schema: "iis",
                table: "StagedLogEntries",
                columns: new[] { "cs-host", "Date", "Time" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntries",
                schema: "iis");

            migrationBuilder.DropTable(
                name: "StagedLogEntries",
                schema: "iis");

            migrationBuilder.DropTable(
                name: "LogFiles",
                schema: "iis");

            migrationBuilder.DropTable(
                name: "Sites",
                schema: "iis");
        }
    }
}
