// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-20-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-20-2026
// ***********************************************************************
// <copyright file="JobSearchLogService.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   EF Core-backed implementation of IJobSearchLogService.
//   All operations are scoped to the current UTC calendar date.
// </summary>
// ***********************************************************************

using Microsoft.EntityFrameworkCore;
using MITJobTracker.Data;
using MITJobTracker.Services.Interfaces;
using DailyJobSearchLog = MITJobTracker.Data.DailyJobSearchLog;

namespace MITJobTracker.Services;

public class JobSearchLogService : IJobSearchLogService
{
    private readonly AppDBContext _context;

    public JobSearchLogService(AppDBContext context)
    {
        _context = context;
    }

    private static DateTime Today => DateTime.UtcNow.Date;

    // ── Retrieval tracking ────────────────────────────────────────────

    public async Task<HashSet<string>> GetTodaysRetrievedJobIdsAsync()
    {
        var today = Today;
        var ids = await _context.DailyJobSearchLogs
            .Where(l => l.SearchDate == today)
            .Select(l => l.ExternalJobId)
            .ToListAsync();

        return new HashSet<string>(ids, StringComparer.OrdinalIgnoreCase);
    }

    public async Task LogRetrievedJobsAsync(IEnumerable<string> externalJobIds)
    {
        var today = Today;

        // Only insert IDs not already logged today (handles concurrent searches)
        var existingIds = await _context.DailyJobSearchLogs
            .Where(l => l.SearchDate == today)
            .Select(l => l.ExternalJobId)
            .ToHashSetAsync(StringComparer.OrdinalIgnoreCase);

        var newEntries = externalJobIds
            .Where(id => !string.IsNullOrWhiteSpace(id) && !existingIds.Contains(id))
            .Select(id => new DailyJobSearchLog
            {
                SearchDate      = today,
                ExternalJobId   = id,
                RetrievedAtUtc  = DateTime.UtcNow
            })
            .ToList();

        if (newEntries.Count > 0)
        {
            _context.DailyJobSearchLogs.AddRange(newEntries);
            await _context.SaveChangesAsync();
        }
    }

    // ── Reviewed tracking ─────────────────────────────────────────────

    public async Task<HashSet<string>> GetTodaysReviewedJobIdsAsync()
    {
        var today = Today;
        var ids = await _context.DailyJobSearchLogs
            .Where(l => l.SearchDate == today && l.IsReviewed)
            .Select(l => l.ExternalJobId)
            .ToListAsync();

        return new HashSet<string>(ids, StringComparer.OrdinalIgnoreCase);
    }

    public async Task MarkJobReviewedAsync(string externalJobId)
    {
        var today = Today;
        var entry = await _context.DailyJobSearchLogs
            .FirstOrDefaultAsync(l => l.SearchDate == today
                                   && l.ExternalJobId == externalJobId);

        if (entry is null)
        {
            entry = new DailyJobSearchLog
            {
                SearchDate      = today,
                ExternalJobId   = externalJobId,
                RetrievedAtUtc  = DateTime.UtcNow
            };
            _context.DailyJobSearchLogs.Add(entry);
        }

        entry.IsReviewed    = true;
        entry.ReviewedAtUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task UnmarkJobReviewedAsync(string externalJobId)
    {
        var today = Today;
        var entry = await _context.DailyJobSearchLogs
            .FirstOrDefaultAsync(l => l.SearchDate == today
                                   && l.ExternalJobId == externalJobId);

        if (entry is not null)
        {
            entry.IsReviewed    = false;
            entry.ReviewedAtUtc = null;
            await _context.SaveChangesAsync();
        }
    }

    // ── Day management ────────────────────────────────────────────────


    // <summary>
    /// Determines whether any <see cref="DailyJobSearchLog"/> records exist
    /// for today's UTC date.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that resolves to <c>true</c> if at least
    /// one record with a <c>SearchDate</c> matching today's UTC date exists in
    /// the database; otherwise <c>false</c>.
    /// </returns>
    public async Task<bool> HasTodaysRecordsAsync()
    {
        var today = Today;
        return await _context.DailyJobSearchLogs
            .AnyAsync(l => l.SearchDate <= today);
    }


    /// <summary>
    /// Removes all <see cref="DailyJobSearchLog"/> records with a
    /// <c>SearchDate <= on or before today's UTC date, effectively
    /// resetting the current day's (and any stale prior-day) search log.
    /// </summary>
    /// <remarks>
    /// This method performs a bulk delete; all retrieved and reviewed
    /// job tracking data up to and including today will be permanently
    /// removed. If no matching records exist, no database write is made.
    /// </remarks>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation

    public async Task ResetDayAsync()
    {
        var today = Today;
        var todaysRecords = await _context.DailyJobSearchLogs
            .Where(l => l.SearchDate <= today)
            .ToListAsync();

        if (todaysRecords.Count > 0)
        {
            _context.DailyJobSearchLogs.RemoveRange(todaysRecords);
            await _context.SaveChangesAsync();
        }
    }
}
