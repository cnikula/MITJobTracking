// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : techn
// Created          : 04-22-2024
//
// Last Modified By : techn
// Last Modified On : 04-22-2024
// ***********************************************************************
// <copyright file="JobsFactory.cs" company="MITJobTracker">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Jobs Factory Class CRUD operations</summary>
// ***********************************************************************

using MITJobTracker.Data;
using MITJobTracker.Factory.Interfaces;
using MITJobTracker.Services.Interfaces;

namespace MITJobTracker.Factory
{
    public class JobsFactory : IJobsFactory
    {
        private readonly IJobsServices _jobsServices;

        public JobsFactory(IJobsServices jobsServices)
        {
            _jobsServices = jobsServices;
        }

        
        public Task<Job> GetJobById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetJobs()
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddJob(Job job)
        {
            if (job is null) throw new ArgumentNullException(nameof(job));
            return await _jobsServices.AddJob(job);
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
