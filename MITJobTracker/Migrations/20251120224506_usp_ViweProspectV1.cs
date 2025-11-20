using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class usp_ViweProspectV1 : Migration
    {
        /// <inheritdoc />
        string sql = string.Empty;

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            sql = @"USE MITJobTracker;
                GO

                SET QUOTED_IDENTIFIER, ANSI_NULLS ON
                GO

                PRINT (N'Create or alter procedure [dbo].[usp_ViewProspect]')
                GO

                CREATE OR ALTER PROCEDURE dbo.usp_ViewProspect
                                @SearchValue NVARCHAR(100) = 'Software', -- Removed the extra quotes
                                @FullList BIT = 1

                AS
                -- ============================================================================
                -- Author:      Claude Nikula
                -- Create date: 5/9/2024 1:59 PM
                -- Database:    MITJobTracker
                -- Description: Perform a search using LIKE operator instead of full-text index
                --              on Jobs table or return a full list of jobs applied
                --
                -- Parameter:  @SearchValue - the search value for the LIKE search
                --             @FullList - A flag indicating whether to query a full list or 
                --                         only the records that contain the search value
                --
                -- Modify: On 7/2/24, By Claude Nikula, Version 2
                --         Add Jobs.CompanyName field to be returned.
                -- Modify: On 11/20/24, By AI Assistant, Version 3
                --         Replaced like with LIKE operator due to missing full-text index.
                -- ===========================================================================
                SET NOCOUNT ON;

              
                IF (@SearchValue IS NULL OR LEN(@SearchValue) = 0)
                BEGIN
                    SET @SearchValue = 'Software'; 
                END

                IF (@FullList IS NULL)
                BEGIN
                    SET @FullList = 1;
                END

                IF (@FullList = 1)
                BEGIN  
                    -- query a full list
                     SELECT j.JobId
                        ,j.JobTitle
                        ,j.JobNo
                        ,j.DateApplied
                        ,CASE WHEN DATEDIFF(DAY, j.DateApplied, GETDATE()) > 21 THEN 'Expired'
                            ELSE 'In Process'
                         END AS Status
                        ,j.JobLocation
                        ,j.RecruitingAgency
                        ,j.RecruitertName
                        ,j.RecruiterPhone
                        ,j.RecruiterEmail
                        ,j.CompanyName
                        ,i.InterviewId
                        ,i.InterviewDate
                        ,i.InterviewType
                       FROM Jobs j
                       LEFT JOIN Interviews i
                         ON j.JobId = i.JobId
                       WHERE j.IsDeleted = 0;
                END
                ELSE  
                 BEGIN
                       -- query only records that contain the key words using LIKE operator
                       SELECT j.JobId
                        ,j.JobTitle
                        ,j.JobNo
                        ,j.DateApplied
                        ,CASE WHEN DATEDIFF(DAY, j.DateApplied, GETDATE()) > 21 THEN 'Expired'
                            ELSE 'In Process'
                         END AS Status
                        ,j.JobLocation
                        ,j.RecruitingAgency
                        ,j.RecruitertName
                        ,j.RecruiterPhone
                        ,j.RecruiterEmail
                        ,j.CompanyName
                        ,i.InterviewId
                        ,i.InterviewDate
                        ,i.InterviewType
                       FROM Jobs j
                       LEFT JOIN Interviews i
                         ON j.JobId = i.JobId
                         WHERE (j.JobTitle LIKE '%' + @SearchValue + '%'
                          OR j.JobLocation LIKE '%' + @SearchValue + '%'
                          OR j.RecruitingAgency LIKE '%' + @SearchValue + '%'
                          OR j.CompanyName LIKE '%' + @SearchValue + '%')
                         AND j.IsDeleted = 0;
                  END 
                GO
                ";
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
