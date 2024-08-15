// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : techn
// Created          : 04-22-2024
//
// Last Modified By : techn
// Last Modified On : 04-22-2024
// ***********************************************************************
// <copyright file="JobsServices.cs" company="MITJobTracker">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Jobs Services Class CRUD operations</summary>
// ***********************************************************************

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MITJobTracker.Data;
using MITJobTracker.Data.DTOS;
using MITJobTracker.Services.Interfaces;
using MITJobTracker.Data.Common;
using System.Buffers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MITJobTracker.Services
{
    public class JobsServices : IJobsServices
    {
        protected readonly AppDBContext _context;
        private CommonSP _commonSP;

        public JobsServices(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _commonSP = new CommonSP(configuration);
        }

       
        
        public Task<Job> GetJobById(int id)
        {
            throw new NotImplementedException();
        }

        
        public async Task<List<ProspectListDTO>> GetJobList(string searchValue, bool fullList)
        {
           return await _commonSP.GetJobList(searchValue, fullList);
        }

       
        /// <summary>Adds the job.</summary>
        /// <param name="job">The job.</param>
        /// <returns>System.Int32.</returns>
        /// <remarks>Add new record to the Database table Jobs</remarks>
        public async Task<int> AddJob(Job job)
        {
            int returnValue = 0;

            try
            {
                //_context.Entry(job).State = EntityState.Added;

                _context.Jobs.Add(job);
                
                returnValue = await _context.SaveChangesAsync();

                if (returnValue > 0)
                {
                    return returnValue;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("");
                Console.WriteLine($"Error Message: {e.Message}, Error No. {e.HResult.ToString()}");
                return 0;
            }
        }

        public Task<Job> UpdateJobById(Job job)
        {
            throw new NotImplementedException();
        }

        public Task<Job> DeleteJob(int id)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Removes expired jobs asynchronously.
        /// </summary>
        /// <param name="jobIds">The list of job IDs to remove.</param>
        /// <returns>The number of removed jobs.</returns>
        /// <remarks>
        /// This method removes expired jobs from the database.
        /// It takes a list of job IDs as input and removes the corresponding jobs from the database.
        /// The job IDs are converted to a comma-separated string and used in the SQL query to remove the jobs.
        /// If any error occurs during the removal process, an error message is printed to the console.
        /// </remarks>
        public async Task<int> RemoveExpiredJobsAsync(List<int> jobIds)
        {
            int returnValue = 0;

            try
            {
                string jobIdsString = string.Join(",", jobIds);

                // Call the appropriate method to remove expired jobs using the jobIdsString
                returnValue = await _commonSP.RemoveExpiredJobsByIdAsync(jobIdsString);

                return returnValue;
            }
            catch (Exception e)
            {
                Console.WriteLine("");
                Console.WriteLine($"Error Message: {e.Message}, Error No. {e.HResult.ToString()}");
                return 0;
            }
        }

        public async Task<JobsInterviewDTO> GetJobInterviewById(int id)
        {
            try
            {
                return await _commonSP.GetJobInterviewById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> UpdateJobAndInterview(JobsInterviewDTO JobsInterview, bool job, bool interview)
        {
            bool returnValue = false;

            if (job && interview)
            {
                
            }
            else if (job && !interview)
            {
                
            }
            else if (interview && !job)
            {
                
            }
            else
            {
                returnValue = false;
            }

            return returnValue;
        }
    }


}

