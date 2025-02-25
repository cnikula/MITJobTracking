﻿// ***********************************************************************
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
        private readonly string connectionString = string.Empty;

        public CommonSP(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("mitLocalConnection");
        }




        /// <summary>
        /// Retrieves a list of job prospects based on the search value and full list flag.
        /// </summary>
        /// <param name="searchValue">The search value to filter the job prospects.</param>
        /// <param name="fullList">A flag indicating whether to retrieve the full list of job prospects.</param>
        /// <returns>A list of ProspectListDTO objects representing the job prospects.</returns>
        public async Task<List<ProspectListDTO>> GetJobList(string searchValue, bool fullList)
        {
            if (string.IsNullOrWhiteSpace(searchValue)) throw new ArgumentNullException(nameof(searchValue));

            List<ProspectListDTO> dt = new List<ProspectListDTO>();

            await using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State != ConnectionState.Open)
                {
                    throw new Exception("Connection to database failed.");
                }

                await using (SqlCommand command = new SqlCommand("dbo.usp_ViewProspect", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SearchValue", searchValue);
                    command.Parameters.AddWithValue("@FullList", fullList);

                    await using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // check if reader has rows
                        if (!reader.HasRows)
                        {
                            return dt;
                        }

                        while (reader.Read())
                        {
                            ProspectListDTO result = new ProspectListDTO
                            {
                                JobId = reader["JobId"] != DBNull.Value ? Convert.ToInt32(reader["JobId"]) : 0,
                                JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : null,
                                JobNo = reader["JobNo"] != DBNull.Value ? reader["JobNo"].ToString() : null,
                                DateApplied = reader["DateApplied"] != DBNull.Value ? Convert.ToDateTime(reader["DateApplied"]) : DateTime.MinValue,
                                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : null,
                                JobLocation = reader["JobLocation"] != DBNull.Value ? reader["JobLocation"].ToString() : null,
                                RecruitingAgency = reader["RecruitingAgency"] != DBNull.Value ? reader["RecruitingAgency"].ToString() : null,
                                RecruitertName = reader["RecruitertName"] != DBNull.Value ? reader["RecruitertName"].ToString() : null,
                                RecruiterPhone = reader["RecruiterPhone"] != DBNull.Value ? reader["RecruiterPhone"].ToString() : null,
                                RecruiterEmail = reader["RecruiterEmail"] != DBNull.Value ? reader["RecruiterEmail"].ToString() : null,
                                InterviewId = reader["InterviewId"] != DBNull.Value ? Convert.ToInt32(reader["InterviewId"]) : 0,
                                InterviewDate = reader["InterviewDate"] != DBNull.Value ? Convert.ToDateTime(reader["InterviewDate"]) : DateTime.MinValue,
                                InterviewType = reader["InterviewType"] != DBNull.Value ? reader["InterviewType"].ToString() : null,
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null

                            };

                            dt.Add(result);
                        }

                    }
                }
            }

            return dt;
        }



        /// <summary>
        /// Removes expired jobs from the database based on the provided job IDs.
        /// </summary>
        /// <param name="jobIds">The IDs of the jobs to be removed.</param>
        /// <returns>The number of jobs that were successfully removed.</returns>
        /// <remarks>jobIds is a list of comma seprated of ID's</remarks>
        public async Task<int> RemoveExpiredJobsByIdAsync(string jobIds)
        {
            // Check if jobIds is null or empty
            if (string.IsNullOrWhiteSpace(jobIds)) throw new ArgumentNullException(nameof(jobIds));

            int returnValue = 0;

            // Open a connection to the database
            await using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if the connection is open
                if (con.State != ConnectionState.Open)
                {
                    throw new Exception("Connection to database failed.");
                }

                // Create a new SqlCommand to call the stored procedure
                await using (SqlCommand command = new SqlCommand("dbo.sp_SoftDeleteJobs", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@JobIds", jobIds);
                    command.Parameters.AddWithValue("@UserId", "CNikula");

                    // Execute the command and retrieve the results
                    await using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Retrieve the return value from the reader
                                returnValue = reader["DeletedCount"] != DBNull.Value
                                    ? Convert.ToInt32(reader["DeletedCount"])
                                    : 0;
                            }
                        }
                    }
                }
            }

            // Return the number of jobs that were successfully removed
            return returnValue;
        }

        /// <summary>
        /// Get detail data from Jobs and Interview tables
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<JobsInterviewDTO> GetJobInterviewById(int id)
        {
            if (id == 0) throw new ArgumentNullException(nameof(id));

            JobsInterviewDTO dt = new JobsInterviewDTO();

            await using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State != ConnectionState.Open)
                {
                    throw new Exception("Connection to database failed.");
                }

                await using (SqlCommand command = new SqlCommand("dbo.sp_GetDetailInfo", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    await using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // check if reader has rows
                        if (!reader.HasRows)
                        {
                            return dt;
                        }

                        while (reader.Read())
                        {
                            JobsInterviewDTO result = new JobsInterviewDTO
                            {
                                JobId = reader["JobId"] != DBNull.Value ? Convert.ToInt32(reader["JobId"]) : 0,
                                JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : null,
                                JobNo = reader["JobNo"] != DBNull.Value ? reader["JobNo"].ToString() : null,
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                RecruitingAgency = reader["RecruitingAgency"] != DBNull.Value ? reader["RecruitingAgency"].ToString() : null,
                                RecruitertName = reader["RecruitertName"] != DBNull.Value ? reader["RecruitertName"].ToString() : null,
                                RecruiterPhone = reader["RecruiterPhone"] != DBNull.Value ? reader["RecruiterPhone"].ToString() : null,
                                RecruiterEmail = reader["RecruiterEmail"] != DBNull.Value ? reader["RecruiterEmail"].ToString() : null,
                                JobLocation = reader["JobLocation"] != DBNull.Value ? reader["JobLocation"].ToString() : null,
                                Remote = reader["Remote"] != DBNull.Value ? Convert.ToBoolean(reader["Remote"]) : false,
                                Hybrid = reader["Hybrid"] != DBNull.Value ? Convert.ToBoolean(reader["Hybrid"]) : false,
                                HybridNoOfDays = reader["HybridNoOfDays"] != DBNull.Value ? reader["HybridNoOfDays"].ToString() : null,
                                Requirements = reader["Requirements"] != DBNull.Value ? reader["Requirements"].ToString() : null,
                                JobDescription = reader["JobDescription"] != DBNull.Value ? reader["JobDescription"].ToString() : null,
                                Salary = reader["Salary"] != DBNull.Value ? reader["Salary"].ToString() : null,
                                EmploymentType = reader["EmploymentType"] != DBNull.Value ? reader["EmploymentType"].ToString() : null,
                                SubContract = reader["SubContract"] != DBNull.Value ? Convert.ToBoolean(reader["SubContract"]) : false,
                                ResumeSend = reader["ResumeSend"] != DBNull.Value ? Convert.ToBoolean(reader["ResumeSend"]) : false,
                                ResumeSendDate = reader["ResumeSendDate"] != DBNull.Value ? Convert.ToDateTime(reader["ResumeSendDate"]) : DateTime.MinValue,
                                DateApplied = reader["DateApplied"] != DBNull.Value ? Convert.ToDateTime(reader["DateApplied"]) : DateTime.MinValue,
                                Duration = reader["Duration"] != DBNull.Value ? reader["Duration"].ToString() : null,
                                OnSite = reader["OnSite"] != DBNull.Value ? Convert.ToBoolean(reader["OnSite"]) : false,
                                Note = reader["Note"] != DBNull.Value ? reader["Note"].ToString() : null,
                                InterviewId = reader["InterviewId"] != DBNull.Value ? Convert.ToInt32(reader["InterviewId"]) : 0,
                                InterviewDate = reader["InterviewDate"] != DBNull.Value ? Convert.ToDateTime(reader["InterviewDate"]) : DateTime.MinValue,
                                InterviewType = reader["InterviewType"] != DBNull.Value ? reader["InterviewType"].ToString() : null,
                                InterviewerName = reader["InterviewerName"] != DBNull.Value ? reader["InterviewerName"].ToString() : null,
                                InterviewerPhone = reader["InterviewerPhone"] != DBNull.Value ? reader["InterviewerPhone"].ToString() : null,
                                InterviewerEmail = reader["InterviewerEmail"] != DBNull.Value ? reader["InterviewerEmail"].ToString() : null,
                                InterviewerNotes = reader["InterviewerNotes"] != DBNull.Value ? reader["InterviewerNotes"].ToString() : null,
                                InterviewerResulte = reader["InterviewerResulte"] != DBNull.Value ? reader["InterviewerResulte"].ToString() : null,
                            };

                            dt = result;
                        } 
                    }
                        
                }
            }

            return dt;
        }


    }
}