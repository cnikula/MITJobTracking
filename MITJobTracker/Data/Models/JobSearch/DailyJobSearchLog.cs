// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-20-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-20-2026
// ***********************************************************************
// <copyright file="DailyJobSearchLog.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Tracks job IDs retrieved and reviewed during a single calendar day.
//   Used to suppress duplicate results across multiple searches per day
//   and to hide jobs the user has already reviewed.
// </summary>
// ***********************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MITJobTracker.Data.Models.JobSearch;

[Index(nameof(LogDate), nameof(ExternalJobId), IsUnique = true)]
public class DailyJobSearchLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>The calendar date this log entry belongs to (date portion only, no time).</summary>
    [Required]
    public DateOnly LogDate { get; set; }

    /// <summary>The job_id returned by the JSearch API.</summary>
    [Required]
    [MaxLength(200)]
    public string ExternalJobId { get; set; } = string.Empty;

    /// <summary>UTC timestamp when this job was first retrieved from the API.</summary>
    public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;

    /// <summary>True when the user has checked the Reviewed checkbox for this job.</summary>
    public bool IsReviewed { get; set; } = false;

    /// <summary>UTC timestamp when the user marked this job as reviewed. Null if not reviewed.</summary>
    public DateTime? ReviewedAt { get; set; }
}