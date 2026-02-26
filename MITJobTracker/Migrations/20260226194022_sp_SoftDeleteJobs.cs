using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class sp_SoftDeleteJobs : Migration
    {
         string sql = String.Empty;
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            sql = @"
            CREATE PROCEDURE dbo.sp_SoftDeleteJobs
                @JobIds NVARCHAR(MAX),
                @UserId NVARCHAR(150)
            AS

            -- =======================================================================
            -- By Claude Nikula Mesquite IT
            -- Created: 2/26/2026 1:46 PM
            --What it does:
            -- Accepts a comma-separated list of Job IDs to delete
            -- Marks both Jobs (parent) and related Interviews (child) as deleted
            -- Preserves all data in the database (soft delete approach)
            -- Updates audit trail fields (who deleted, when)
            -- Returns count of affected records
            -- =======================================================================

            BEGIN
                SET NOCOUNT ON;
                
                DECLARE @DeleteDate DATETIME2 = GETDATE();
                DECLARE @DeletedJobCount INT = 0;
                DECLARE @DeletedInterviewCount INT = 0;
                
                BEGIN TRY
                    BEGIN TRANSACTION;
                    
                    -- Soft delete interviews (child records first)
                    UPDATE i
                    SET 
                        i.IsDeleted = 1,
                        i.DeleteByID = @UserId,
                        i.DeleteDate = @DeleteDate,
                        i.ModifiedById = @UserId,
                        i.ModifiedDate = @DeleteDate
                    FROM Interviews i
                    INNER JOIN STRING_SPLIT(@JobIds, ',') s ON i.JobId = CAST(s.value AS INT)
                    WHERE i.IsDeleted = 0;
                    
                    SET @DeletedInterviewCount = @@ROWCOUNT;
                    
                    -- Soft delete jobs (parent records)
                    UPDATE j
                    SET 
                        j.IsDeleted = 1,
                        j.DeleteByID = @UserId,
                        j.DeleteDate = @DeleteDate,
                        j.ModifiedById = @UserId,
                        j.ModifiedDate = @DeleteDate
                    FROM Jobs j
                    INNER JOIN STRING_SPLIT(@JobIds, ',') s ON j.JobId = CAST(s.value AS INT)
                    WHERE j.IsDeleted = 0;
                    
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
                        
                    -- Re-throw the error
                    THROW;
                END CATCH
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
