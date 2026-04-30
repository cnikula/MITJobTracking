// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-28-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-28-2026
// ***********************************************************************
// <copyright file="USAJOBSJobSearchAnnouncement.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Represents a job announcement record returned by the USAJOBS API.
// </summary>
// ***********************************************************************

using System.Text.Json.Serialization;

namespace MITJobTracker.Data.Models.JobSearch;

/// <summary>
/// Represents a detailed job announcement returned by the USAJOBS search API.
/// </summary>
public sealed class USAJOBSJobSearchAnnouncementResponse
{
    /// <summary>Gets or sets the unique USAJOBS control number for this announcement.</summary>
    [JsonPropertyName("usajobsControlNumber")]
    public long UsajobsControlNumber { get; set; }

    /// <summary>Gets or sets the date the position opened (ISO 8601 date string, e.g. "2024-03-22").</summary>
    [JsonPropertyName("positionOpenDate")]
    public string? PositionOpenDate { get; set; }

    /// <summary>Gets or sets the date the position closes (ISO 8601 date string, e.g. "2024-04-01").</summary>
    [JsonPropertyName("positionCloseDate")]
    public string? PositionCloseDate { get; set; }

    /// <summary>Gets or sets the hiring agency code (e.g. "ARAT").</summary>
    [JsonPropertyName("hiringAgencyCode")]
    public string? HiringAgencyCode { get; set; }

    /// <summary>Gets or sets the hiring department code (e.g. "AR").</summary>
    [JsonPropertyName("hiringDepartmentCode")]
    public string? HiringDepartmentCode { get; set; }

    /// <summary>Gets or sets the OPM occupational series code (e.g. "0830").</summary>
    [JsonPropertyName("positionSeries")]
    public string? PositionSeries { get; set; }

    /// <summary>Gets or sets the announcement number assigned by the agency.</summary>
    [JsonPropertyName("announcementNumber")]
    public string? AnnouncementNumber { get; set; }

    /// <summary>Gets or sets the brief summary / overview of the position.</summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    /// <summary>Gets or sets the full duties description.</summary>
    [JsonPropertyName("duties")]
    public string? Duties { get; set; }

    /// <summary>Gets or sets an explanation of the eligible hiring paths for this announcement.</summary>
    [JsonPropertyName("hiringPathExplanation")]
    public string? HiringPathExplanation { get; set; }

    /// <summary>Gets or sets an optional bullet-point list of major duties; may be <see langword="null"/>.</summary>
    [JsonPropertyName("majorDutiesList")]
    public string? MajorDutiesList { get; set; }

    /// <summary>Gets or sets the conditions of employment requirements.</summary>
    [JsonPropertyName("requirementsConditionsOfEmployment")]
    public string? RequirementsConditionsOfEmployment { get; set; }

    /// <summary>Gets or sets the qualifications requirements text.</summary>
    [JsonPropertyName("requirementsQualifications")]
    public string? RequirementsQualifications { get; set; }

    /// <summary>Gets or sets the education requirements text.</summary>
    [JsonPropertyName("requirementsEducation")]
    public string? RequirementsEducation { get; set; }

    /// <summary>Gets or sets any required standard documents; may be <see langword="null"/>.</summary>
    [JsonPropertyName("requiredStandardDocuments")]
    public string? RequiredStandardDocuments { get; set; }

    /// <summary>Gets or sets the free-text list of required documents to submit.</summary>
    [JsonPropertyName("requiredDocuments")]
    public string? RequiredDocuments { get; set; }

    /// <summary>Gets or sets instructions on how to apply for the position.</summary>
    [JsonPropertyName("howToApply")]
    public string? HowToApply { get; set; }

    /// <summary>Gets or sets the next-steps description shown after applying.</summary>
    [JsonPropertyName("howToApplyNextSteps")]
    public string? HowToApplyNextSteps { get; set; }

    /// <summary>Gets or sets supplemental requirements text; may be <see langword="null"/>.</summary>
    [JsonPropertyName("requirements")]
    public string? Requirements { get; set; }

    /// <summary>Gets or sets the basis and method of evaluation for applicants.</summary>
    [JsonPropertyName("evaluations")]
    public string? Evaluations { get; set; }

    /// <summary>Gets or sets the URL linking to federal employee benefits information.</summary>
    [JsonPropertyName("benefitsURL")]
    public string? BenefitsUrl { get; set; }

    /// <summary>Gets or sets a custom benefits description; may be <see langword="null"/>.</summary>
    [JsonPropertyName("benefits")]
    public string? Benefits { get; set; }

    /// <summary>Gets or sets any other information relevant to the announcement.</summary>
    [JsonPropertyName("otherInformation")]
    public string? OtherInformation { get; set; }

    /// <summary>Gets or sets an override for the appointment type; may be <see langword="null"/>.</summary>
    [JsonPropertyName("appointmentTypeOverride")]
    public string? AppointmentTypeOverride { get; set; }

    /// <summary>Gets or sets an override for the position schedule; may be <see langword="null"/>.</summary>
    [JsonPropertyName("positionScheduleOverride")]
    public string? PositionScheduleOverride { get; set; }

    /// <summary>Gets or sets clarification text for exclusive announcements; may be <see langword="null"/>.</summary>
    [JsonPropertyName("exclusiveClarificationText")]
    public string? ExclusiveClarificationText { get; set; }

    /// <summary>Gets or sets the URL of an associated recruitment or informational video.</summary>
    [JsonPropertyName("videoURL")]
    public string? VideoUrl { get; set; }
}
