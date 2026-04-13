// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-10-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-10-2026
// ***********************************************************************
// <copyright file="JobSearchResponse.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Data models for the external job search API response</summary>
// ***********************************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace MITJobTracker.Data.Models.JobSearch;

public class JobSearchResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("request_id")]
    public string RequestId { get; set; } = string.Empty;

    [JsonPropertyName("parameters")]
    public JobSearchParameters Parameters { get; set; } = new();

    [JsonPropertyName("data")]
    public List<JobListing> Data { get; set; } = [];
}

public class JobSearchParameters
{
    [JsonPropertyName("query")]
    public string Query { get; set; } = string.Empty;

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("num_pages")]
    public int NumPages { get; set; }

    [JsonPropertyName("date_posted")]
    public string DatePosted { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;
}

public class JobListing
{
    [JsonPropertyName("job_id")]
    public string JobId { get; set; } = string.Empty;

    [JsonPropertyName("job_title")]
    public string JobTitle { get; set; } = string.Empty;

    [JsonPropertyName("employer_name")]
    public string EmployerName { get; set; } = string.Empty;

    [JsonPropertyName("employer_logo")]
    public string? EmployerLogo { get; set; }

    [JsonPropertyName("employer_website")]
    public string? EmployerWebsite { get; set; }

    [JsonPropertyName("job_publisher")]
    public string JobPublisher { get; set; } = string.Empty;

    [JsonPropertyName("job_employment_type")]
    public string JobEmploymentType { get; set; } = string.Empty;

    [JsonPropertyName("job_employment_types")]
    public List<string> JobEmploymentTypes { get; set; } = [];

    [JsonPropertyName("job_apply_link")]
    public string JobApplyLink { get; set; } = string.Empty;

    [JsonPropertyName("job_apply_is_direct")]
    public bool JobApplyIsDirect { get; set; }

    [JsonPropertyName("apply_options")]
    public List<ApplyOption> ApplyOptions { get; set; } = [];

    [JsonPropertyName("job_description")]
    public string JobDescription { get; set; } = string.Empty;

    [JsonPropertyName("job_is_remote")]
    public bool JobIsRemote { get; set; }

    [JsonPropertyName("job_posted_at")]
    public string? JobPostedAt { get; set; }

    [JsonPropertyName("job_posted_at_timestamp")]
    public long? JobPostedAtTimestamp { get; set; }

    [JsonPropertyName("job_posted_at_datetime_utc")]
    public DateTime? JobPostedAtDateTimeUtc { get; set; }

    [JsonPropertyName("job_location")]
    public string JobLocation { get; set; } = string.Empty;

    [JsonPropertyName("job_city")]
    public string JobCity { get; set; } = string.Empty;

    [JsonPropertyName("job_state")]
    public string JobState { get; set; } = string.Empty;

    [JsonPropertyName("job_country")]
    public string JobCountry { get; set; } = string.Empty;

    [JsonPropertyName("job_latitude")]
    public double? JobLatitude { get; set; }

    [JsonPropertyName("job_longitude")]
    public double? JobLongitude { get; set; }

    [JsonPropertyName("job_benefits")]
    public List<string>? JobBenefits { get; set; }

    [JsonPropertyName("job_benefits_strings")]
    public List<string>? JobBenefitsStrings { get; set; }

    [JsonPropertyName("job_google_link")]
    public string JobGoogleLink { get; set; } = string.Empty;

    [JsonPropertyName("job_salary")]
    public JsonElement? JobSalary { get; set; }

    [JsonPropertyName("job_salary_string")]
    public string? JobSalaryString { get; set; }

    [JsonPropertyName("job_min_salary")]
    public decimal? JobMinSalary { get; set; }

    [JsonPropertyName("job_max_salary")]
    public decimal? JobMaxSalary { get; set; }

    [JsonPropertyName("job_salary_period")]
    public string? JobSalaryPeriod { get; set; }

    [JsonPropertyName("job_highlights")]
    public Dictionary<string, JsonElement> JobHighlights { get; set; } = [];

    [JsonPropertyName("job_onet_soc")]
    public string? JobOnetSoc { get; set; }

    [JsonPropertyName("job_onet_job_zone")]
    public string? JobOnetJobZone { get; set; }

    [JsonPropertyName("employer_reviews")]
    public JsonElement? EmployerReviews { get; set; }
}

public class ApplyOption
{
    [JsonPropertyName("apply_link")]
    public string ApplyLink { get; set; } = string.Empty;

    [JsonPropertyName("is_direct")]
    public bool IsDirect { get; set; }

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; } = string.Empty;
}