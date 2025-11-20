using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class sp_GetDetailInfo_Ver1 : Migration
    {
        string sql = String.Empty;

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                            sql = @"USE MITJobTracker
                GO

                IF DB_NAME() <> N'MITJobTracker' SET NOEXEC ON
                GO

                SET QUOTED_IDENTIFIER, ANSI_NULLS ON
                GO

                PRINT (N'Create or alter procedure [dbo].[GetDetailInfo]')
                GO
                CREATE OR ALTER PROCEDURE dbo.sp_GetDetailInfo
                  @Id INTEGER
                AS 
                -- ============================================================================
                 -- Author:      Claude Nikula
                 -- Create date: 7/30/2024 1:52 PM
                 -- Database:    MITJobTracker
                 -- Description: 
                 --
                 -- Parameter:  @Id - primary key for the Jobs table-
                 --
                 -- Modify:
                 -- ===========================================================================

                SET NOCOUNT ON

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
                 WHERE j.JobId = @Id

                GO";

                migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            sql = @"USE MITJobTracker
                    GO

                    IF DB_NAME() <> N'MITJobTracker' SET NOEXEC ON
                    GO

                    --
                    -- Drop procedure [dbo].[sp_GetDetailInfo]
                    --
                    GO
                    DROP PROCEDURE IF EXISTS dbo.sp_GetDetailInfo
                    GO";

            migrationBuilder.Sql(sql);
        }
    }
}
