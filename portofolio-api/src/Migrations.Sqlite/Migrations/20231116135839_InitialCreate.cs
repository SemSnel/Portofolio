using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemSnel.Portofolio.Migrations.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "DomainMessages",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProcessedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdempotentRequests",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RequestId = table.Column<string>(type: "TEXT", nullable: false),
                    RequestBody = table.Column<string>(type: "TEXT", nullable: false),
                    RequestHeaders = table.Column<string>(type: "TEXT", nullable: false),
                    ResponseBody = table.Column<string>(type: "TEXT", nullable: true),
                    ResponseStatusCode = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdempotentRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    ProcessedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    ProcessedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    TemperatureC = table.Column<int>(type: "INTEGER", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainMessages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IdempotentRequests",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InboxMessages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WeatherForecasts");
        }
    }
}
