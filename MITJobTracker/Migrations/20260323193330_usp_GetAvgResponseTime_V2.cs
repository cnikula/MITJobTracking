using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class usp_GetAvgResponseTime_V2 : Migration
    {
        private string sql = "";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            sql = @"
                CREATE OR ALTER PROCEDURE dbo.usp_GetAvgResponseTime
                AS

                -- =======================================================================
                -- By Claude Nikula Mesquite IT
                -- Created: 3/23/2026
                -- Database: MITJobTracker
                -- Version 2.0.0
                -- What it does:
                -- Calculates the average response time in days between the date a job
                -- was applied for (Jobs.DateApplied) and the first interview date
                -- (Interviews.InterviewDate).
                -- Formula: AVG(DATEDIFF(DAY, j.DateApplied, i.InterviewDate))
                -- Filter: Only interviews on or after 1/1/2026
                -- Only counts active (non-deleted) jobs and interviews
                -- Returns a single integer value (AvgResponseDays)
                -- Returns 0 if no matching records are found
                -- =======================================================================

                BEGIN
                    SET NOCOUNT ON;

                    DECLARE @AvgResponseDays INT = 0;

                    SELECT @AvgResponseDays = AVG(DATEDIFF(DAY, j.DateApplied, i.InterviewDate))
                    FROM dbo.Interviews i
                    INNER JOIN dbo.Jobs j ON i.JobId = j.JobId
                    WHERE i.InterviewDate >= '2026-01-01';


                    SELECT ISNULL(@AvgResponseDays, 0) AS AvgResponseDays;
                END";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.usp_GetAvgResponseTime;");
        }
    }
}
