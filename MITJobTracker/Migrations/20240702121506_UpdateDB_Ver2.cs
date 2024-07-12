using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB_Ver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecruiterPhone",
                table: "Jobs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Recruiter Phone, the agency person you are communicating with phone number.",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "Recruiter Phone, the agency person you are communicating with phone number.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecruiterPhone",
                table: "Jobs",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "Recruiter Phone, the agency person you are communicating with phone number.",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "Recruiter Phone, the agency person you are communicating with phone number.");
        }
    }
}
