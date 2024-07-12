// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : techn
// Created          : 04-22-2024
//
// Last Modified By : techn
// Last Modified On : 04-22-2024
// ***********************************************************************
// <copyright file="IJobsServices.cs" company="MITJobTracker">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Interfaces Jobs Services Class CRUD operations</summary>
// ***********************************************************************

using MITJobTracker.Data.DTOS;
using MITJobTracker.Data;

namespace MITJobTracker.Services.Interfaces
{
    public interface IJobsServices
    {
        Task<Job> GetJobById(int id);
        Task<List<ProspectListDTO>> GetJobList(string searchValue, bool fullList);
        Task<int> AddJob(Job job);
        Task<Job> UpdateJobById(Job job);
        Task<Job> DeleteJob(int id);
        Task<int> RemoveExpiredJobsAsync(List<int> jobIds);
    }
}
