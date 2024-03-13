using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class JobDB_Ver1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false, comment: "Table primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false, comment: "Job Title, description of the title"),
                    JobNo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true, comment: "Job Number, position identifier for the job you are appalling for."),
                    CompanyName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "Company Name, the company name you are applying for."),
                    RecruitingAgency = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "Recruiting Agency, the agency that is helping you to get the job. Note this can be the same as CompanyName "),
                    RecruitertName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Recruiter Name, the agency person you are communicating with"),
                    RecruiterPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Recruiter Phone, the agency person you are communicating with phone number."),
                    RecruiterEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "Recruiter Email, the agency person you are communicating with email."),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Job Description, the job description."),
                    JobLocation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "Job Location, the job location."),
                    Remote = table.Column<bool>(type: "bit", nullable: false, comment: "Remote, if the job is remote."),
                    Hybrid = table.Column<bool>(type: "bit", nullable: false, comment: "Hybrid, if the job is hybrid."),
                    HybridNoOfDays = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true, comment: "Hybrid No Of Days, how may days on-site"),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Job Requirements, the job requirements."),
                    Salary = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true, comment: "Salary, pay range per hour our salary"),
                    EmploymentType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Employment Type, W2, 1099 Part-Time, Full-Time."),
                    SubContract = table.Column<bool>(type: "bit", nullable: false, comment: "Sub-Contract, if you contract through another company. Example Head-hunter"),
                    ResumeSend = table.Column<bool>(type: "bit", nullable: false, comment: "Resume Send, did you send a resume out"),
                    ResumeSendDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Resume Send Date, the date you send the resume out"),
                    CreatedById = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "External foreign."),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and Time record was create."),
                    ModifiedById = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "External foreign."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and Time record was create."),
                    DeleteByID = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "External foreign."),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and Time record was create.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    InterviewId = table.Column<int>(type: "int", nullable: false, comment: "InterviewId, Table primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false, comment: "JobId, foreign key to the Jobs table"),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Interview Date, the date of the interview"),
                    InterviewType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Interview Type, is interview by phone, in-person or via computer. Example Zoome, Teams, Sky "),
                    InterviewerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Interviewer Name, the person full name that is interviewing you"),
                    InterviewerPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true, comment: "Interviewer Phone, the person phone number that is interviewing you"),
                    InterviewerEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "Interviewer Email, the person email that is interviewing you"),
                    InterviewerNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "Interviewer Notes, any notes that you want to keep about the interview"),
                    InterviewerResulte = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "Interviewer Result, the result of the interview"),
                    CreatedById = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "External foreign."),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and Time record was create."),
                    ModifiedById = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "External foreign."),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and Time record was create."),
                    DeleteByID = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "External foreign."),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date and Time record was create.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.InterviewId);
                    table.ForeignKey(
                        name: "FK_Interviews_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_JobId",
                table: "Interviews",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
