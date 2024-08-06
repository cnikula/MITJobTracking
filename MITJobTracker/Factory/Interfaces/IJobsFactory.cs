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
using MITJobTracker.Data.DTOS;

namespace MITJobTracker.Factory.Interfaces
{
    public interface IJobsFactory
    {
        Task<Job> GetJobById(int id);
        Task<List<ProspectListDTO>> GetJobList(string searchValue, bool fullList);
        Task<int> AddJob(Job job);
        Task<Job> UpdateJobById(Job job);
        Task<Job> DeleteJob(int id);
        Task<int> RemoveExpiredJobsAsync(List<int> jobIds);
        Task<JobsInterviewDTO> GetJobInterviewById(int id);
    }
}
