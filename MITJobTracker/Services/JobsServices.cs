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

        public Task<Job> GetJobById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetJobs()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a new job to the database
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task<int> AddJob(Job job)
        {
            int returnValue = 0;

            try
            {
                using (var context = new AppDBContext(new DbContextOptions<AppDBContext>()))
                {
                    context.Jobs.Add(job);
                    returnValue = await context.SaveChangesAsync();

                    if (returnValue > 0)
                    {
                        return returnValue;
                    }
                    else
                    {
                        return 0;
                    }
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

