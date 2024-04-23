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


using Microsoft.EntityFrameworkCore;
using MITJobTracker.Data;
using MITJobTracker.Services.Interfaces;

namespace MITJobTracker.Services
{
    public class JobsServices : IJobsServices
    {
        private readonly AppDBContext _context;

        public JobsServices(AppDBContext context)
        {
            _context = context;
        }


        public Task<Job> GetJobById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetJobs()
        {
            throw new NotImplementedException();
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

    }


}

