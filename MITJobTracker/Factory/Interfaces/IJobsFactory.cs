// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : techn
// Created          : 04-22-2024
//
// Last Modified By : techn
// Last Modified On : 04-22-2024
// ***********************************************************************
// <copyright file="IJobsFactory.cs" company="MITJobTracker">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Interface for the Jobs Factory Class</summary>
// ***********************************************************************


using MITJobTracker.Data;

namespace MITJobTracker.Factory.Interfaces
{
    public interface IJobsFactory
    {
        Task<Job> GetJobById(int id);
        Task<List<Job>> GetJobs();
        Task<int> AddJob(Job job);
        Task<Job> UpdateJobById(Job job);
        Task<Job> DeleteJob(int id);
    }
}
