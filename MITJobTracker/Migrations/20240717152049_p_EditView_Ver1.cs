using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class p_EditView_Ver1 : Migration
    {
        private string sql = String.Empty;

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            sql = @"CREATE PROCEDURE dbo.sp_EditView
                @JobID INT

                AS 

                  -- ============================================================================
                  -- Author:      Claude Nikula
                  -- Create date: 7/17/2024 @ 9:11 AM
                  -- Database:    MITJobTracker
                  -- Description: Get Job and Interview data buy JobID key
                  --
                  -- Parameter:  @JobID - Job Key
                  --             
                  --
                  -- Modify: On 7/17/2024, Version 1.0.0
                  --        
                  -- ===========================================================================

                SET NOCOUNT ON

                BEGIN

                  SELECT j.JobId
                        ,j.JobTitle
                        ,j.JobNo
                        ,j.CompanyName
                        ,j.RecruitingAgency
                        ,j.RecruitertName
                        ,j.RecruiterPhone
                        ,j.RecruiterEmail
                        ,j.JobDescription
                        ,j.JobLocation
                        ,j.Remote
                        ,j.Hybrid
                        ,j.HybridNoOfDays
                        ,j.Requirements
                        ,j.Salary
                        ,j.EmploymentType
                        ,j.SubContract
                        ,j.ResumeSend
                        ,j.ResumeSendDate
                        ,j.DateApplied
                        ,j.Duration
                        ,j.OnSite
                        ,i.InterviewId
                        ,i.InterviewDate
                        ,i.InterviewType
                        ,i.InterviewerName
                        ,i.InterviewerPhone
                        ,i.InterviewerEmail
                        ,i.InterviewerNotes
                        ,i.InterviewerResulte
                     FROM Jobs j
                      LEFT JOIN Interviews i
                        ON j.JobId = i.JobId
                     WHERE j.JobId = @JobID
                        AND j.IsDeleted = 0 

	                
                END


                GO";
           
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            sql = @"DROP PROCEDURE dbo.sp_EditView";
            migrationBuilder.Sql(sql);
        }
    }
}
