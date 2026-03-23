// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 05-16-2024
//
// Last Modified By : Claude Nikula
// Last Modified On : 05-22-2024
// ***********************************************************************
// <copyright file="CommonSP.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//          Class for calling Stored Procedures. SP are not handled by
//          Entity Framework. This class is used to call SPs and return
//          data
// </summary>
// ***********************************************************************

using System.Data;
using Microsoft.Data.SqlClient;
using MITJobTracker.Data.DTOS;

namespace MITJobTracker.Data.Common
{
    public class CommonSP
    {
        private readonly string _connectionString;
        private const int CommandTimeout = 30; // seconds

        public CommonSP(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("mitLocalConnection") 
                ?? throw new InvalidOperationException("Connection string 'mitLocalConnection' not found");
        }




        /// <summary>
        /// Retrieves a list of job prospects based on the search value and full list flag.
        /// </summary>
        /// <param name="searchValue">The search value to filter the job prospects.</param>
        /// <param name="fullList">A flag indicating whether to retrieve the full list of job prospects.</param>
        /// <returns>A list of ProspectListDTO objects representing the job prospects.</returns>
        public async Task<List<ProspectListDTO>> GetJobList(string searchValue, bool fullList, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchValue)) 
                throw new ArgumentNullException(nameof(searchValue));

            var results = new List<ProspectListDTO>();

            await using var con = new SqlConnection(_connectionString);
            await con.OpenAsync(cancellationToken);

            await using var command = new SqlCommand("dbo.usp_ViewProspect", con)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = CommandTimeout
            };

            command.Parameters.Add("@SearchValue", SqlDbType.NVarChar, 255).Value = searchValue;
            command.Parameters.Add("@FullList", SqlDbType.Bit).Value = fullList;

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                results.Add(new ProspectListDTO
                {
                    JobId = reader.GetInt32Safe("JobId"),
                    JobTitle = reader.GetStringSafe("JobTitle"),
                    JobNo = reader.GetStringSafe("JobNo"),
                    DateApplied = reader.GetDateTimeSafe("DateApplied"),
                    Status = reader.GetStringSafe("Status"),
                    JobLocation = reader.GetStringSafe("JobLocation"),
                    RecruitingAgency = reader.GetStringSafe("RecruitingAgency"),
                    RecruitertName = reader.GetStringSafe("RecruitertName"),
                    RecruiterPhone = reader.GetStringSafe("RecruiterPhone"),
                    RecruiterEmail = reader.GetStringSafe("RecruiterEmail"),
                    InterviewId = reader.GetInt32Safe("InterviewId"),
                    InterviewDate = reader.GetDateTimeSafe("InterviewDate"),
                    InterviewType = reader.GetStringSafe("InterviewType"),
                    CompanyName = reader.GetStringSafe("CompanyName")
                });
            }

            return results;
        }



        /// <summary>
        /// Removes expired jobs by their IDs. (Soft delete only, not physical delete from database)
        /// </summary>
        /// <param name="jobIds">A comma-separated list of job IDs to be removed.</param>
        /// <param name="userId">The ID of the user performing the deletion.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The number of jobs that were marked as deleted.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> RemoveExpiredJobsByIdAsync(string jobIds, string userId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(jobIds)) 
                throw new ArgumentNullException(nameof(jobIds));
            if (string.IsNullOrWhiteSpace(userId)) 
                throw new ArgumentNullException(nameof(userId));

            await using var con = new SqlConnection(_connectionString);
            await con.OpenAsync(cancellationToken);

            await using var command = new SqlCommand("dbo.sp_SoftDeleteJobs", con)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = CommandTimeout
            };

            command.Parameters.Add("@JobIds", SqlDbType.NVarChar, -1).Value = jobIds;
            command.Parameters.Add("@UserId", SqlDbType.NVarChar, 150).Value = userId;

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (await reader.ReadAsync(cancellationToken))
            {
                return reader.GetInt32Safe("DeletedCount");
            }

            return 0;
        }

        /// <summary>
        /// Get detail data from Jobs and Interview tables
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<JobsInterviewDTO?> GetJobInterviewById(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) 
                throw new ArgumentException("ID must be greater than zero", nameof(id));

            await using var con = new SqlConnection(_connectionString);
            await con.OpenAsync(cancellationToken);

            await using var command = new SqlCommand("dbo.sp_GetDetailInfo", con)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = CommandTimeout
            };

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (await reader.ReadAsync(cancellationToken))
            {
                return new JobsInterviewDTO
                {
                    JobId = reader.GetInt32Safe("JobId"),
                    JobTitle = reader.GetStringSafe("JobTitle"),
                    JobNo = reader.GetStringSafe("JobNo"),
                    CompanyName = reader.GetStringSafe("CompanyName"),
                    RecruitingAgency = reader.GetStringSafe("RecruitingAgency"),
                    RecruitertName = reader.GetStringSafe("RecruitertName"),
                    RecruiterPhone = reader.GetStringSafe("RecruiterPhone"),
                    RecruiterEmail = reader.GetStringSafe("RecruiterEmail"),
                    JobLocation = reader.GetStringSafe("JobLocation"),
                    Remote = reader.GetBooleanSafe("Remote"),
                    Hybrid = reader.GetBooleanSafe("Hybrid"),
                    HybridNoOfDays = reader.GetStringSafe("HybridNoOfDays"),
                    Requirements = reader.GetStringSafe("Requirements"),
                    JobDescription = reader.GetStringSafe("JobDescription"),
                    Salary = reader.GetStringSafe("Salary"),
                    EmploymentType = reader.GetStringSafe("EmploymentType"),
                    SubContract = reader.GetBooleanSafe("SubContract"),
                    ResumeSend = reader.GetBooleanSafe("ResumeSend"),
                    ResumeSendDate = reader.GetDateTimeSafe("ResumeSendDate"),
                    DateApplied = reader.GetDateTimeSafe("DateApplied"),
                    Duration = reader.GetStringSafe("Duration"),
                    OnSite = reader.GetBooleanSafe("OnSite"),
                    Note = reader.GetStringSafe("Note"),
                    InterviewId = reader.GetInt32Safe("InterviewId"),
                    InterviewDate = reader.GetDateTimeSafe("InterviewDate"),
                    InterviewType = reader.GetStringSafe("InterviewType"),
                    InterviewerName = reader.GetStringSafe("InterviewerName"),
                    InterviewerPhone = reader.GetStringSafe("InterviewerPhone"),
                    InterviewerEmail = reader.GetStringSafe("InterviewerEmail"),
                    InterviewerNotes = reader.GetStringSafe("InterviewerNotes"),
                    InterviewerResulte = reader.GetStringSafe("InterviewerResulte")
                };
            }

            return null;
        }

        // <summary>
        /// Gets the interview rate by calling the usp_GetInterviewRate stored procedure.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The interview rate as a decimal percentage.</returns>
        public async Task<decimal> GetInterviewRateAsync(CancellationToken cancellationToken = default)
        {
            await using var con = new SqlConnection(_connectionString);
            await con.OpenAsync(cancellationToken);

            await using var command = new SqlCommand("dbo.usp_GetInterviewRate", con)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = CommandTimeout
            };

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (await reader.ReadAsync(cancellationToken))
            {
                var ordinal = reader.GetOrdinal("InterviewRate");
                return reader.IsDBNull(ordinal) ? 0 : reader.GetDecimal(ordinal);
            }

            return 0;
        }

        /// <summary>
        /// Asynchronously retrieves the average response time from the database.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the average response time in
        /// milliseconds, or 0 if no data is available.</returns>
        public async Task<int> GetAvgResponseTimeAsync(CancellationToken cancellationToken = default)
        {
            await using var con = new SqlConnection(_connectionString);
            await con.OpenAsync(cancellationToken);

            await using var command = new SqlCommand("dbo.usp_GetAvgResponseTime", con)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = CommandTimeout
            };

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (await reader.ReadAsync(cancellationToken))
            {
                var ordinal = reader.GetOrdinal("AvgResponseDays");
                return reader.IsDBNull(ordinal) ? 0 : reader.GetInt32(ordinal);
            }

            return 0;
        }


    }

    // Extension methods for safer data reader access
    public static class SqlDataReaderExtensions
    {
        public static string? GetStringSafe(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        public static int GetInt32Safe(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? 0 : reader.GetInt32(ordinal);
        }

        public static bool GetBooleanSafe(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return !reader.IsDBNull(ordinal) && reader.GetBoolean(ordinal);
        }

        public static DateTime GetDateTimeSafe(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? DateTime.MinValue : reader.GetDateTime(ordinal);
        }

       

    }
}