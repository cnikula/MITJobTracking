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
        private readonly string connectionString = string.Empty;

        public CommonSP(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("mitLocalConnection");
        }



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
                                InterviewType = reader["InterviewType"] != DBNull.Value ? reader["InterviewType"].ToString() : null

                            };

                            dt.Add(result);
                        }

                    }
                }
            }
            return dt;
        }
    }
}
