using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class Update_Jobs_New_Field_Ver4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecruiterPhone",
                table: "Jobs",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "Recruiter Phone, the agency person you are communicating with phone number.",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true,
                oldComment: "Recruiter Phone, the agency person you are communicating with phone number.");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: " IsDeleted, Is record deleted.");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Interviews",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: " IsDeleted, Is record deleted.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Interviews");

            migrationBuilder.AlterColumn<string>(
                name: "RecruiterPhone",
                table: "Jobs",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                comment: "Recruiter Phone, the agency person you are communicating with phone number.",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "Recruiter Phone, the agency person you are communicating with phone number.");
        }
    }
}
