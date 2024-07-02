using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class sp_ViewProspect_Ver2 : Migration
    {
        string sql = string.Empty;

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                 sql = @"USE MITJobTracker19
                        GO

                        IF DB_NAME() <> N'MITJobTracker19' SET NOEXEC ON
                        GO

                        SET QUOTED_IDENTIFIER, ANSI_NULLS ON
                        GO

                        --
                        -- Create or alter procedure [dbo].[usp_ViewProspect]
                        --
                        GO
                        PRINT (N'Create or alter procedure [dbo].[usp_ViewProspect]')
                        GO
                        CREATE OR ALTER PROCEDURE dbo.usp_ViewProspect
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
                                        --
                                        -- Modify: On 7/2/24, By Claude Nikula, Version 2
                                        --         Add Jobs.CompanyName field to be returned.
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
                                                ,j.CompanyName
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
                                                ,j.CompanyName
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
