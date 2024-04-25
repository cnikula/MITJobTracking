using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class Jobs_Table_Update_Ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecruiterPhone",
                table: "Jobs",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                comment: "Recruiter Phone, the agency person you are communicating with phone number.",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldComment: "Recruiter Phone, the agency person you are communicating with phone number.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecruiterPhone",
                table: "Jobs",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                comment: "Recruiter Phone, the agency person you are communicating with phone number.",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true,
                oldComment: "Recruiter Phone, the agency person you are communicating with phone number.");
        }
    }
}
