// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-17-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-17-2026
// ***********************************************************************
// <copyright file="IJobSearchLogService.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Interface for managing daily job-search retrieval logs.
//   Supports duplicate suppression, "Reviewed" marking, and
//   daily reset operations.
// </summary>
// ***********************************************************************

namespace MITJobTracker.Services.Interfaces;

public interface IJobSearchLogService
{
    /// <summary>
    /// Returns the set of external job IDs already retrieved today (UTC).
    /// </summary>
    Task<HashSet<string>> GetTodaysRetrievedJobIdsAsync();

    /// <summary>
    /// Returns the set of external job IDs marked as reviewed today (UTC).
    /// </summary>
    Task<HashSet<string>> GetTodaysReviewedJobIdsAsync();

    /// <summary>
    /// Logs a batch of newly retrieved external job IDs for today.
    /// Existing IDs for today are ignored (idempotent).
    /// </summary>
    Task LogRetrievedJobsAsync(IEnumerable<string> externalJobIds);

    /// <summary>
    /// Marks a single job as reviewed for today.
    /// </summary>
    Task MarkJobReviewedAsync(string externalJobId);

    /// <summary>
    /// Un-marks a single job as reviewed for today (undo).
    /// </summary>
    Task UnmarkJobReviewedAsync(string externalJobId);

    /// <summary>
    /// Deletes all log records for today (manual "Reset Day" button).
    /// </summary>
    Task ResetDayAsync();

    /// <summary>
    /// Returns true if any records exist for today (UTC).
    /// Used to determine whether the automatic first-search-of-day
    /// reset has already occurred.
    /// </summary>
    Task<bool> HasTodaysRecordsAsync();
}
