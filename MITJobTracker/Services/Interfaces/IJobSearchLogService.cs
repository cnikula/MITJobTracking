// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-20-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-20-2026
// ***********************************************************************
// <copyright file="IJobSearchLogService.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Contract for the daily job search log service.
//   Manages duplicate suppression and reviewed-job tracking
//   backed by the DailyJobSearchLogs SQL Server table.
// </summary>
// ***********************************************************************

namespace MITJobTracker.Services.Interfaces;

public interface IJobSearchLogService
{
    /// <summary>Returns all external job IDs already retrieved today.</summary>
    Task<HashSet<string>> GetTodaysRetrievedJobIdsAsync();

    /// <summary>Logs a batch of newly retrieved job IDs for today.</summary>
    Task LogRetrievedJobsAsync(IEnumerable<string> externalJobIds);

    /// <summary>Returns all external job IDs marked as reviewed today.</summary>
    Task<HashSet<string>> GetTodaysReviewedJobIdsAsync();

    /// <summary>Marks a single job as reviewed.</summary>
    Task MarkJobReviewedAsync(string externalJobId);

    /// <summary>Removes the reviewed flag from a single job.</summary>
    Task UnmarkJobReviewedAsync(string externalJobId);

    /// <summary>Returns true if any log records exist for today (used to detect first search of the day).</summary>
    Task<bool> HasTodaysRecordsAsync();

    /// <summary>Deletes all log records for today, resetting duplicate suppression and reviewed state.</summary>
    Task ResetDayAsync();
}
