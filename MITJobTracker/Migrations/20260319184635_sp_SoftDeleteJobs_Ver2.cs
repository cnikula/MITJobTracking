using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class sp_SoftDeleteJobs_Ver2 : Migration
    {
        private string sql = "";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            sql = @"CREATE OR ALTER PROCEDURE dbo.sp_SoftDeleteJobs
                @JobIds NVARCHAR(MAX),
                @UserId NVARCHAR(150)
                AS
                -- =======================================================================
                -- Procedure: sp_SoftDeleteJobs Version 2
                -- By Claude Nikula Mesquite IT
                -- Description: Soft deletes jobs and their related interviews based on 
                -- provided Job IDs
                -- Created: 2/26/2026 1:46 PM
                -- Optimized: Performance improvements with temp table and validation
                -- =======================================================================
                BEGIN
                    SET NOCOUNT ON;
    
                    -- Validate inputs
                    IF @JobIds IS NULL OR LTRIM(RTRIM(@JobIds)) = ''
                    BEGIN
                        RAISERROR('JobIds parameter cannot be null or empty', 16, 1);
                        RETURN;
                    END
    
                    IF @UserId IS NULL OR LTRIM(RTRIM(@UserId)) = ''
                    BEGIN
                        RAISERROR('UserId parameter cannot be null or empty', 16, 1);
                        RETURN;
                    END
    
                    DECLARE @DeleteDate DATETIME2 = SYSUTCDATETIME(); -- Faster than GETDATE()
                    DECLARE @DeletedJobCount INT = 0;
                    DECLARE @DeletedInterviewCount INT = 0;
    
                    -- Create temp table to parse IDs once (major performance improvement)
                    CREATE TABLE #JobIdsToDelete (
                        JobId INT PRIMARY KEY  -- Clustered index for fast lookups
                    );
    
                    -- Parse the comma-separated list once
                    INSERT INTO #JobIdsToDelete (JobId)
                    SELECT DISTINCT CAST(value AS INT)
                    FROM STRING_SPLIT(@JobIds, ',')
                    WHERE LTRIM(RTRIM(value)) <> ''  -- Skip empty values
                      AND ISNUMERIC(value) = 1;       -- Validate numeric
    
                    -- Check if any valid IDs were provided
                    IF NOT EXISTS (SELECT 1 FROM #JobIdsToDelete)
                    BEGIN
                        RAISERROR('No valid Job IDs provided', 16, 1);
                        RETURN;
                    END
    
                    BEGIN TRY
                        BEGIN TRANSACTION;
        
                        -- Soft delete interviews (child records first)
                        -- Use EXISTS for better performance with indexes
                        UPDATE i
                        SET 
                            i.IsDeleted = 1,
                            i.DeleteByID = @UserId,
                            i.DeleteDate = @DeleteDate,
                            i.ModifiedById = @UserId,
                            i.ModifiedDate = @DeleteDate
                        FROM Interviews i WITH (ROWLOCK)  -- Row-level locking
                        WHERE i.IsDeleted = 0
                          AND EXISTS (SELECT 1 FROM #JobIdsToDelete t WHERE t.JobId = i.JobId);
        
                        SET @DeletedInterviewCount = @@ROWCOUNT;
        
                        -- Soft delete jobs (parent records)
                        UPDATE j
                        SET 
                            j.IsDeleted = 1,
                            j.DeleteByID = @UserId,
                            j.DeleteDate = @DeleteDate,
                            j.ModifiedById = @UserId,
                            j.ModifiedDate = @DeleteDate
                        FROM Jobs j WITH (ROWLOCK)  -- Row-level locking
                        WHERE j.IsDeleted = 0
                          AND EXISTS (SELECT 1 FROM #JobIdsToDelete t WHERE t.JobId = j.JobId);
        
                        SET @DeletedJobCount = @@ROWCOUNT;
        
                        COMMIT TRANSACTION;
        
                        -- Return the counts
                        SELECT 
                            @DeletedJobCount AS DeletedCount,
                            @DeletedInterviewCount AS DeletedInterviewCount;
            
                    END TRY
                    BEGIN CATCH
                        IF @@TRANCOUNT > 0
                            ROLLBACK TRANSACTION;
        
                        -- Clean up temp table
                        IF OBJECT_ID('tempdb..#JobIdsToDelete') IS NOT NULL
                            DROP TABLE #JobIdsToDelete;
            
                        -- Re-throw the error with context
                        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
                        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
                        DECLARE @ErrorState INT = ERROR_STATE();
        
                        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
                    END CATCH
    
                    -- Clean up temp table
                    IF OBJECT_ID('tempdb..#JobIdsToDelete') IS NOT NULL
                        DROP TABLE #JobIdsToDelete;
                END
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
               -- Drop procedure [dbo].[sp_SoftDeleteJobs]
               --
               GO
               DROP PROCEDURE IF EXISTS dbo.sp_SoftDeleteJobs
               GO";

            migrationBuilder.Sql(sql);
        }
    }
}
