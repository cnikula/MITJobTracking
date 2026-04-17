// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-17-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-17-2026
// ***********************************************************************
// <copyright file="JobSearchLogService.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Database-backed implementation of IJobSearchLogService.
//   All date comparisons use the UTC calendar day.
// </summary>
// ***********************************************************************

using Microsoft.EntityFrameworkCore;
using MITJobTracker.Data;
using MITJobTracker.Services.Interfaces;

namespace MITJobTracker.Services;

public class JobSearchLogService : IJobSearchLogService
{
    private readonly AppDBContext _context;

    public JobSearchLogService(AppDBContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<HashSet<string>> GetTodaysRetrievedJobIdsAsync()
    {
        var today = DateTime.UtcNow.Date;
        var ids = await _context.DailyJobSearchLogs
            .Where(l => l.SearchDate == today)
            .Select(l => l.ExternalJobId)
            .ToListAsync();
        return new HashSet<string>(ids, StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public async Task<HashSet<string>> GetTodaysReviewedJobIdsAsync()
    {
        var today = DateTime.UtcNow.Date;
        var ids = await _context.DailyJobSearchLogs
            .Where(l => l.SearchDate == today && l.IsReviewed)
            .Select(l => l.ExternalJobId)
            .ToListAsync();
        return new HashSet<string>(ids, StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public async Task LogRetrievedJobsAsync(IEnumerable<string> externalJobIds)
    {
        var today = DateTime.UtcNow.Date;
        var utcNow = DateTime.UtcNow;

        var existing = await _context.DailyJobSearchLogs
            .Where(l => l.SearchDate == today)
            .Select(l => l.ExternalJobId)
            .ToListAsync();

        var existingSet = new HashSet<string>(existing, StringComparer.OrdinalIgnoreCase);

        var newLogs = externalJobIds
            .Where(id => !existingSet.Contains(id))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Select(id => new DailyJobSearchLog
            {
                ExternalJobId = id,
                SearchDate = today,
                RetrievedAtUtc = utcNow,
                IsReviewed = false
            })
            .ToList();

        if (newLogs.Count > 0)
        {
            _context.DailyJobSearchLogs.AddRange(newLogs);
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public async Task MarkJobReviewedAsync(string externalJobId)
    {
        var today = DateTime.UtcNow.Date;
        var log = await _context.DailyJobSearchLogs
            .FirstOrDefaultAsync(l => l.SearchDate == today && l.ExternalJobId == externalJobId);

        if (log is not null && !log.IsReviewed)
        {
            log.IsReviewed = true;
            log.ReviewedAtUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public async Task UnmarkJobReviewedAsync(string externalJobId)
    {
        var today = DateTime.UtcNow.Date;
        var log = await _context.DailyJobSearchLogs
            .FirstOrDefaultAsync(l => l.SearchDate == today && l.ExternalJobId == externalJobId);

        if (log is not null && log.IsReviewed)
        {
            log.IsReviewed = false;
            log.ReviewedAtUtc = null;
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public async Task ResetDayAsync()
    {
        var today = DateTime.UtcNow.Date;
        var todaysLogs = await _context.DailyJobSearchLogs
            .Where(l => l.SearchDate == today)
            .ToListAsync();

        if (todaysLogs.Count > 0)
        {
            _context.DailyJobSearchLogs.RemoveRange(todaysLogs);
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public async Task<bool> HasTodaysRecordsAsync()
    {
        var today = DateTime.UtcNow.Date;
        return await _context.DailyJobSearchLogs.AnyAsync(l => l.SearchDate == today);
    }
}
