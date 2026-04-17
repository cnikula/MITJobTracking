using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyJobSearchLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyJobSearchLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Table primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalJobId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "External job ID returned by the search API"),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Calendar date (UTC) on which the job was retrieved"),
                    RetrievedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "UTC timestamp when the record was first created"),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false, comment: "True when the user has marked this job as reviewed"),
                    ReviewedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "UTC timestamp when the job was marked reviewed (null if not reviewed)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyJobSearchLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProspectListDTO",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateApplied = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecruitingAgency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecruitertName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecruiterPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecruiterEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterviewId = table.Column<int>(type: "int", nullable: false),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterviewType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyJobSearchLogs_ExternalJobId_SearchDate",
                table: "DailyJobSearchLogs",
                columns: new[] { "ExternalJobId", "SearchDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyJobSearchLogs");

            migrationBuilder.DropTable(
                name: "ProspectListDTO");
        }
    }
}
