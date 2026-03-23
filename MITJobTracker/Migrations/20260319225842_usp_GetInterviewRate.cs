using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class usp_GetInterviewRate : Migration
    {
        private string sql = "";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            sql = @"CREATE OR ALTER PROCEDURE dbo.usp_GetInterviewRate
                AS

                 -- =======================================================================
                 -- By Claude Nikula Mesquite IT
                 -- Created: 3/19/2026
                 -- Database: MITJobTracker
                 -- What it does:
                 -- Calculates the Interview Rate percentage.
                 -- Interview Rate = (Distinct jobs with interviews / Total active jobs) * 100
                 -- Only counts non-deleted (active) jobs.
                 -- Returns a single decimal value (InterviewRate) rounded to 1 decimal place.
                 -- Returns 0 if there are no active jobs.
                 -- =======================================================================

                BEGIN
                    SET NOCOUNT ON;

                    DECLARE @TotalJobs INT;
                    DECLARE @JobsWithInterviews INT;
                    DECLARE @InterviewRate DECIMAL(5,1);

                    -- Count active (non-deleted) jobs
                    SELECT @TotalJobs = COUNT(*)
                    FROM dbo.Jobs
                    WHERE IsDeleted = 0;

                    IF @TotalJobs = 0
                    BEGIN
                        SELECT CAST(0 AS DECIMAL(5,1)) AS InterviewRate;
                        RETURN;
                    END

                    -- Count distinct jobs that have at least one interview
                    SELECT @JobsWithInterviews = COUNT(DISTINCT i.JobId)
                    FROM dbo.Interviews i
                    INNER JOIN dbo.Jobs j ON i.JobId = j.JobId
                    WHERE j.IsDeleted = 0;

                    -- Calculate the interview rate
                    SET @InterviewRate = ROUND(CAST(@JobsWithInterviews AS DECIMAL(10,2)) / @TotalJobs * 100, 1);

                    SELECT @InterviewRate AS InterviewRate;
                END";

            migrationBuilder.Sql(sql);
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
             sql = @"USE MITJobTracker
                GO

                IF DB_NAME() <> N'MITJobTracker' SET NOEXEC ON
                GO

                --
                -- Drop procedure [dbo].[usp_GetInterviewRate]
                --
                GO
                DROP PROCEDURE IF EXISTS dbo.usp_GetInterviewRate
                GO";

            migrationBuilder.Sql(sql);
        }
    }
}
