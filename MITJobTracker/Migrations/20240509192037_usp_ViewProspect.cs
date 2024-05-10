using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class usp_ViewProspect : Migration
    {
        string sql = string.Empty;
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            sql = @"CREATE OR ALTER PROCEDURE dbo.usp_ViewProspect
                    @SearchValue NVARCHAR(100) = '""Software""'

                    AS
                    -- =============================================
                    -- Author:      Claude Nikula
                    -- Create date: 5/9/2024 1:59 PM
                    -- Database:    MITJobTracker19
                    -- Description: Perform a full text search on
                    --              Jobs table 
                    -- =============================================


                      BEGIN
  	                    SET NOCOUNT ON

                           Select j.JobId
                            ,j.JobTitle
                            ,j.JobNo
                            ,j.DateApplied
                            ,j.JobLocation
                            ,j.RecruitingAgency
                            ,j.RecruitertName
                            ,j.RecruiterPhone
                            ,j.RecruiterEmail
                            ,i.InterviewId
                            ,i.InterviewDate
                            ,i.InterviewType
                           from Jobs j
                           LEFT JOIN Interviews i
                             ON j.JobId = i.JobId
                            where contains (j.*,@SearchValue)
                              AND j.IsDeleted = 0
                      END 
                    GO";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            sql = @"DROP PROCEDURE dbo.usp_ViewProspect";
            migrationBuilder.Sql(sql);
        }
    }
}
