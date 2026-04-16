// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-10-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-10-2026
// ***********************************************************************
// <copyright file="JobSearchRequest.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Request parameters model for the JSearch external API</summary>
// ***********************************************************************

namespace MITJobTracker.Data.Models.JobSearch;

public class JobSearchRequest
{
    /// <summary>Required. Free-form search query including title and location.</summary>
    public string Query { get; set; } = string.Empty;

    /// <summary>Page to return (1–50). Default: 1</summary>
    public int Page { get; set; } = 1;

    /// <summary>Number of pages to return starting from Page (1–50). Default: 1</summary>
    public int NumPages { get; set; } = 1;

    /// <summary>ISO 3166-1 alpha-2 country code. Default: us</summary>
    public string Country { get; set; } = "us";

    /// <summary>ISO 639 language code. Leave empty for country default.</summary>
    public string Language { get; set; } = string.Empty;

    /// <summary>Location from which the search is made, e.g. "San Diego, United States"</summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>Filter by post date. Allowed: all, today, 3days, week, month</summary>
    public string DatePosted { get; set; } = "all";

    /// <summary>Only return remote/work-from-home jobs.</summary>
    public bool WorkFromHome { get; set; } = false;

    /// <summary>Comma-delimited employment types: FULLTIME, CONTRACTOR, PARTTIME, INTERN</summary>
    public List<string> EmploymentTypes { get; set; } = [];

    /// <summary>Comma-delimited requirements: under_3_years_experience, more_than_3_years_experience, no_experience, no_degree</summary>
    public List<string> JobRequirements { get; set; } = [];

    /// <summary>Return jobs within this distance (km) from the query location.</summary>
    public int? Radius { get; set; }

    /// <summary>Comma-separated list of publishers to exclude, e.g. "Dice,BeeBe"</summary>
    public string ExcludeJobPublishers { get; set; } = string.Empty;
}