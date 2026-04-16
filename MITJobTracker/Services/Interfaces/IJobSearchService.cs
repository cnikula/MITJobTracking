// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-10-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-10-2026
// ***********************************************************************
// <copyright file="IJobSearchService.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Interface for the external JSearch API service</summary>
// ***********************************************************************

using MITJobTracker.Data.Models.JobSearch;

namespace MITJobTracker.Services.Interfaces;

public interface IJobSearchService
{
    Task<JobSearchResponse?> SearchJobsAsync(JobSearchRequest request);
}