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
                @SearchValue NVARCHAR(100) = '""Software""',
                @FullList BIT = 1

                AS
                -- ============================================================================
                -- Author:      Claude Nikula
                -- Create date: 5/9/2024 1:59 PM
                -- Database:    MITJobTracker19
                -- Description: Perform a full text search on
                --              Jobs table or
                --              return a full list of jobs applied
                --
                -- Parameter:  @SearchValue - the search value for the full-text index search
                --             @FullList - A flag indicating whether to query a full list or 
                --                         only the records that contain the search value
                -- ===========================================================================
                SET NOCOUNT ON

                IF (@SearchValue IS NULL OR LEN(@SearchValue) = 0)
                BEGIN
                    SET @SearchValue = '""Software""'
                END

                IF (@FullList IS NULL)
                BEGIN
                    SET @FullList = 1;
                END

                IF (@FullList = 1)
                BEGIN  
                    -- query a full list
	                 Select j.JobId
                        ,j.JobTitle
                        ,j.JobNo
                        ,j.DateApplied
                        ,CASE WHEN Datediff(DAY, j.DateApplied, GETDATE()) > 21 THEN 'Expired'
                            ELSE 'In Process'
                         END AS Status
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
                       WHERE j.IsDeleted = 0
                END
                ELSE  
                 BEGIN
                       -- quewry only contains the key words in the full text index
                       Select j.JobId
                        ,j.JobTitle
                        ,j.JobNo
                        ,j.DateApplied
                        ,CASE WHEN Datediff(DAY, j.DateApplied, GETDATE()) > 21 THEN 'Expired'
                            ELSE 'In Process'
                         END AS Status
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
                        where FREETEXT (j.*,@SearchValue)
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
