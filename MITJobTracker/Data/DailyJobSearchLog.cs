// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-17-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-17-2026
// ***********************************************************************
// <copyright file="DailyJobSearchLog.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Entity that tracks which external job IDs have been retrieved
//   (and optionally reviewed) on a given day.  Used to suppress
//   duplicate results within the same calendar day.
// </summary>
// ***********************************************************************

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MITJobTracker.Data;

[Index(nameof(ExternalJobId), nameof(SearchDate))]
public class DailyJobSearchLog
{
    [Comment("Table primary key")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Comment("External job ID returned by the search API")]
    [Required]
    [MaxLength(256)]
    public string ExternalJobId { get; set; } = string.Empty;

    [Comment("Calendar date (UTC) on which the job was retrieved")]
    public DateTime SearchDate { get; set; }

    [Comment("UTC timestamp when the record was first created")]
    public DateTime RetrievedAtUtc { get; set; }

    [Comment("True when the user has marked this job as reviewed")]
    public bool IsReviewed { get; set; }

    [Comment("UTC timestamp when the job was marked reviewed (null if not reviewed)")]
    public DateTime? ReviewedAtUtc { get; set; }
}
