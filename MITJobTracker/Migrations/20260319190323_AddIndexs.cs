using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITJobTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexs : Migration
    {
        private string sql = "";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
              sql = @"-- On Interviews table
                CREATE NONCLUSTERED INDEX IX_Interviews_JobId_IsDeleted 
                ON Interviews(JobId, IsDeleted) 
                INCLUDE (DeleteByID, DeleteDate, ModifiedById, ModifiedDate);

                -- On Jobs table
                CREATE NONCLUSTERED INDEX IX_Jobs_JobId_IsDeleted 
                ON Jobs(JobId, IsDeleted) 
                INCLUDE (DeleteByID, DeleteDate, ModifiedById, ModifiedDate);";

            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            sql = @"DROP INDEX IX_Interviews_JobId_IsDeleted ON Interviews;
                    DROP INDEX IX_Jobs_JobId_IsDeleted ON Jobs;";
            
            migrationBuilder.Sql(sql);

        }
    }
}
