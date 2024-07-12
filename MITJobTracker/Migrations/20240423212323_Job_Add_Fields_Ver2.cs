using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class Job_Add_Fields_Ver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ResumeSendDate",
                table: "Jobs",
                type: "datetime2",
                nullable: true,
                comment: "Resume Send Date, the date you send the resume out or applied for the job. If resume send then ResumeSend is set to 1 (true)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Resume Send Date, the date you send the resume out");

            migrationBuilder.AlterColumn<string>(
                name: "EmploymentType",
                table: "Jobs",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                comment: "Employment Type, W2, 1099, Part-Time, Full-Time.",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldComment: "Employment Type, W2, 1099 Part-Time, Full-Time.");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApplied",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "DateApplied, the date you Applied for the job or send bid.");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Jobs",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                comment: "Duration, how long the job is for.");

            migrationBuilder.AddColumn<bool>(
                name: "OnSite",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "OnSite, if the job is on site.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateApplied",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "OnSite",
                table: "Jobs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ResumeSendDate",
                table: "Jobs",
                type: "datetime2",
                nullable: true,
                comment: "Resume Send Date, the date you send the resume out",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Resume Send Date, the date you send the resume out or applied for the job. If resume send then ResumeSend is set to 1 (true)");

            migrationBuilder.AlterColumn<string>(
                name: "EmploymentType",
                table: "Jobs",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                comment: "Employment Type, W2, 1099 Part-Time, Full-Time.",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldComment: "Employment Type, W2, 1099, Part-Time, Full-Time.");
        }
    }
}
