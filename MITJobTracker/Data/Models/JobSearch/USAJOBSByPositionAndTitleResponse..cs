// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-28-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-28-2026
// ***********************************************************************
// <copyright file="USAJOBSByPositionAndTitle.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Represents the root response returned by the USAJOBS search API
//   when querying by position and title.
// </summary>
// ***********************************************************************

using System.Text.Json.Serialization;

namespace MITJobTracker.Data.Models.JobSearch;

/// <summary>
/// Represents the root response object returned by the USAJOBS search API.
/// </summary>
public sealed class USAJOBSByPositionAndTitleResponse
{
    /// <summary>Gets or sets the BCP 47 language code for the response (e.g. "EN").</summary>
    [JsonPropertyName("LanguageCode")]
    public string? LanguageCode { get; set; }

    /// <summary>Gets or sets the search parameters echoed back by the API.</summary>
    [JsonPropertyName("SearchParameters")]
    public USAJOBSSearchParametersResponse SearchParameters { get; set; } = new();

    /// <summary>Gets or sets the search result payload.</summary>
    [JsonPropertyName("SearchResult")]
    public USAJOBSSearchResultResponse SearchResult { get; set; } = new();
}

/// <summary>
/// Represents the search parameters echoed back in the USAJOBS search response.
/// </summary>
public sealed class USAJOBSSearchParametersResponse;

/// <summary>
/// Represents the search result payload within a USAJOBS search response.
/// </summary>
public sealed class USAJOBSSearchResultResponse
{
    /// <summary>Gets or sets the number of items returned in this page.</summary>
    [JsonPropertyName("SearchResultCount")]
    public int SearchResultCount { get; set; }

    /// <summary>Gets or sets the total number of matching items across all pages.</summary>
    [JsonPropertyName("SearchResultCountAll")]
    public int SearchResultCountAll { get; set; }

    /// <summary>Gets or sets the collection of matched job items.</summary>
    [JsonPropertyName("SearchResultItems")]
    public List<USAJOBSSearchResultItemResponse> SearchResultItems { get; set; } = [];
}

/// <summary>
/// Represents a single matched job item in the USAJOBS search results.
/// </summary>
public sealed class USAJOBSSearchResultItemResponse
{
    /// <summary>Gets or sets the unique identifier for this matched object.</summary>
    [JsonPropertyName("MatchedObjectId")]
    public string MatchedObjectId { get; set; } = string.Empty;

    /// <summary>Gets or sets the descriptor containing the full position details.</summary>
    [JsonPropertyName("MatchedObjectDescriptor")]
    public USAJOBSMatchedObjectDescriptorResponse MatchedObjectDescriptor { get; set; } = new();

    /// <summary>Gets or sets the relevance rank assigned to this result.</summary>
    [JsonPropertyName("RelevanceRank")]
    public int RelevanceRank { get; set; }
}

/// <summary>
/// Represents the full set of details for a single USAJOBS position.
/// </summary>
public sealed class USAJOBSMatchedObjectDescriptorResponse
{
    /// <summary>Gets or sets the unique position identifier (e.g. "DLAJ6-26-12942005-DHA").</summary>
    [JsonPropertyName("PositionID")]
    public string PositionID { get; set; } = string.Empty;

    /// <summary>Gets or sets the human-readable title for this position.</summary>
    [JsonPropertyName("PositionTitle")]
    public string PositionTitle { get; set; } = string.Empty;

    /// <summary>Gets or sets the URI to the full job announcement on USAJOBS.</summary>
    [JsonPropertyName("PositionURI")]
    public string PositionURI { get; set; } = string.Empty;

    /// <summary>Gets or sets the list of URIs used to apply for this position.</summary>
    [JsonPropertyName("ApplyURI")]
    public List<string> ApplyURI { get; set; } = [];

    /// <summary>Gets or sets a display string summarising the position location(s).</summary>
    [JsonPropertyName("PositionLocationDisplay")]
    public string? PositionLocationDisplay { get; set; }

    /// <summary>Gets or sets the collection of specific locations for this position.</summary>
    [JsonPropertyName("PositionLocation")]
    public List<USAJOBSPositionLocationResponse> PositionLocation { get; set; } = [];

    /// <summary>Gets or sets the name of the hiring organisation (e.g. "Defense Logistics Agency").</summary>
    [JsonPropertyName("OrganizationName")]
    public string? OrganizationName { get; set; }

    /// <summary>Gets or sets the name of the parent department (e.g. "Department of Defense").</summary>
    [JsonPropertyName("DepartmentName")]
    public string? DepartmentName { get; set; }

    /// <summary>Gets or sets the sub-agency code or name, if applicable.</summary>
    [JsonPropertyName("SubAgency")]
    public string? SubAgency { get; set; }

    /// <summary>Gets or sets the job category / occupational series for this position.</summary>
    [JsonPropertyName("JobCategory")]
    public List<USAJOBSCodeEntryResponse> JobCategory { get; set; } = [];

    /// <summary>Gets or sets the pay grade codes (e.g. "GS", "ND") for this position.</summary>
    [JsonPropertyName("JobGrade")]
    public List<USAJOBSCodeEntryResponse> JobGrade { get; set; } = [];  
    /// <summary>Gets or sets the work-schedule codes for this position (e.g. full-time = "1").</summary>
    [JsonPropertyName("PositionSchedule")]
    public List<USAJOBSNameCodeEntryResponse> PositionSchedule { get; set; } = [];

    /// <summary>Gets or sets the appointment-type codes for this position.</summary>
    [JsonPropertyName("PositionOfferingType")]
    public List<USAJOBSNameCodeEntryResponse> PositionOfferingType { get; set; } = [];
    /// <summary>Gets or sets the qualification summary text.</summary>
    [JsonPropertyName("QualificationSummary")]
    public string? QualificationSummary { get; set; }

    /// <summary>Gets or sets the salary / remuneration ranges for this position.</summary>
    [JsonPropertyName("PositionRemuneration")]
    public List<USAJOBSPositionRemunerationResponse> PositionRemuneration { get; set; } = [];

    /// <summary>Gets or sets the date on which the position listing becomes active.</summary>
    [JsonPropertyName("PositionStartDate")]
    public DateTimeOffset? PositionStartDate { get; set; }

    /// <summary>Gets or sets the date on which the position listing expires.</summary>
    [JsonPropertyName("PositionEndDate")]
    public DateTimeOffset? PositionEndDate { get; set; }

    /// <summary>Gets or sets the date on which the announcement was first published.</summary>
    [JsonPropertyName("PublicationStartDate")]
    public DateTimeOffset? PublicationStartDate { get; set; }

    /// <summary>Gets or sets the date after which applications are no longer accepted.</summary>
    [JsonPropertyName("ApplicationCloseDate")]
    public DateTimeOffset? ApplicationCloseDate { get; set; }

    /// <summary>Gets or sets the formatted description entries (e.g. dynamic teaser).</summary>
    [JsonPropertyName("PositionFormattedDescription")]
    public List<USAJOBSFormattedDescriptionResponse> PositionFormattedDescription { get; set; } = [];

    /// <summary>Gets or sets the extended user-area details for this position.</summary>
    [JsonPropertyName("UserArea")]
    public USAJOBSUserAreaResponse? UserArea { get; set; }
}

/// <summary>
/// Represents a geographic location associated with a USAJOBS position.
/// </summary>
public sealed class USAJOBSPositionLocationResponse
{
    /// <summary>Gets or sets the human-readable location name (e.g. "Battle Creek, Michigan").</summary>
    [JsonPropertyName("LocationName")]
    public string? LocationName { get; set; }

    /// <summary>Gets or sets the country code (e.g. "United States").</summary>
    [JsonPropertyName("CountryCode")]
    public string? CountryCode { get; set; }

    /// <summary>Gets or sets the state or territory subdivision code (e.g. "Michigan").</summary>
    [JsonPropertyName("CountrySubDivisionCode")]
    public string? CountrySubDivisionCode { get; set; }

    /// <summary>Gets or sets the city name with state (e.g. "Battle Creek, Michigan").</summary>
    [JsonPropertyName("CityName")]
    public string? CityName { get; set; }

    /// <summary>Gets or sets the longitude of this location.</summary>
    [JsonPropertyName("Longitude")]
    public double? Longitude { get; set; }

    /// <summary>Gets or sets the latitude of this location.</summary>
    [JsonPropertyName("Latitude")]
    public double? Latitude { get; set; }
}

/// <summary>
/// Represents a code-only entry used for job grades and similar simple code lists.
/// </summary>
public sealed class USAJOBSCodeEntryResponse
{
    /// <summary>Gets or sets the display name for this entry.</summary>
    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    /// <summary>Gets or sets the short code for this entry (e.g. "GS", "2210").</summary>
    [JsonPropertyName("Code")]
    public string Code { get; set; } = string.Empty;
}

/// <summary>
/// Represents a name/code pair used for schedule types, offering types, and similar lists.
/// </summary>
public sealed class USAJOBSNameCodeEntryResponse
{
    /// <summary>Gets or sets the human-readable name for this entry.</summary>
    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    /// <summary>Gets or sets the short code for this entry.</summary>
    [JsonPropertyName("Code")]
    public string Code { get; set; } = string.Empty;
}

/// <summary>
/// Represents a salary or remuneration range for a USAJOBS position.
/// </summary>
public sealed class USAJOBSPositionRemunerationResponse
{
    /// <summary>Gets or sets the minimum salary or pay as a string (e.g. "125776").</summary>
    [JsonPropertyName("MinimumRange")]
    public string? MinimumRange { get; set; }

    /// <summary>Gets or sets the maximum salary or pay as a string (e.g. "187093").</summary>
    [JsonPropertyName("MaximumRange")]
    public string? MaximumRange { get; set; }

    /// <summary>Gets or sets the rate interval code (e.g. "PA" for Per Annum).</summary>
    [JsonPropertyName("RateIntervalCode")]
    public string? RateIntervalCode { get; set; }

    /// <summary>Gets or sets the human-readable description of the interval (e.g. "Per Year").</summary>
    [JsonPropertyName("Description")]
    public string? Description { get; set; }
}

/// <summary>
/// Represents a formatted description label entry for a USAJOBS position.
/// </summary>
public sealed class USAJOBSFormattedDescriptionResponse
{
    /// <summary>Gets or sets the label identifier (e.g. "Dynamic Teaser").</summary>
    [JsonPropertyName("Label")]
    public string? Label { get; set; }

    /// <summary>Gets or sets the description of what this label represents.</summary>
    [JsonPropertyName("LabelDescription")]
    public string? LabelDescription { get; set; }
}

/// <summary>
/// Represents the user-area wrapper that contains extended position details.
/// </summary>
public sealed class USAJOBSUserAreaResponse
{
    /// <summary>Gets or sets the extended details for this position.</summary>
    [JsonPropertyName("Details")]
    public USAJOBSPositionDetailsResponse? Details { get; set; }

    /// <summary>Gets or sets a value indicating whether this result came from a radial/geo search.</summary>
    [JsonPropertyName("IsRadialSearch")]
    public bool IsRadialSearch { get; set; }
}

/// <summary>
/// Represents the extended position details contained within the user area of a USAJOBS result.
/// </summary>
public sealed class USAJOBSPositionDetailsResponse
{
    /// <summary>Gets or sets the brief job summary text.</summary>
    [JsonPropertyName("JobSummary")]
    public string? JobSummary { get; set; }

    /// <summary>Gets or sets who may apply for this position.</summary>
    [JsonPropertyName("WhoMayApply")]
    public USAJOBSNameCodeEntryResponse? WhoMayApply { get; set; }

    /// <summary>Gets or sets the lowest grade level for this position (e.g. "14").</summary>
    [JsonPropertyName("LowGrade")]
    public string? LowGrade { get; set; }

    /// <summary>Gets or sets the highest grade level for this position (e.g. "14").</summary>
    [JsonPropertyName("HighGrade")]
    public string? HighGrade { get; set; }

    /// <summary>Gets or sets the promotion potential grade level (e.g. "14").</summary>
    [JsonPropertyName("PromotionPotential")]
    public string? PromotionPotential { get; set; }

    /// <summary>Gets or sets the sub-agency display name.</summary>
    [JsonPropertyName("SubAgencyName")]
    public string? SubAgencyName { get; set; }

    /// <summary>Gets or sets the organisation code path (e.g. "DD/DD07").</summary>
    [JsonPropertyName("OrganizationCodes")]
    public string? OrganizationCodes { get; set; }

    /// <summary>Gets or sets a string indicating whether relocation is offered ("True"/"False").</summary>
    [JsonPropertyName("Relocation")]
    public string? Relocation { get; set; }

    /// <summary>Gets or sets the hiring-path codes for this position (e.g. ["public", "vet"]).</summary>
    [JsonPropertyName("HiringPath")]
    public List<string> HiringPath { get; set; } = [];

    /// <summary>Gets or sets the Marketing Campaign Office tag identifiers.</summary>
    [JsonPropertyName("MCOTags")]
    public List<string> MCOTags { get; set; } = [];

    /// <summary>Gets or sets the total number of openings as a string (e.g. "1", "Many").</summary>
    [JsonPropertyName("TotalOpenings")]
    public string? TotalOpenings { get; set; }

    /// <summary>Gets or sets the agency marketing statement text.</summary>
    [JsonPropertyName("AgencyMarketingStatement")]
    public string? AgencyMarketingStatement { get; set; }

    /// <summary>Gets or sets the travel code for this position.</summary>
    [JsonPropertyName("TravelCode")]
    public string? TravelCode { get; set; }

    /// <summary>Gets or sets the URL used to apply online.</summary>
    [JsonPropertyName("ApplyOnlineUrl")]
    public string? ApplyOnlineUrl { get; set; }

    /// <summary>Gets or sets the URL used to check application status details.</summary>
    [JsonPropertyName("DetailStatusUrl")]
    public string? DetailStatusUrl { get; set; }

    /// <summary>Gets or sets the list of major duties for this position.</summary>
    [JsonPropertyName("MajorDuties")]
    public List<string> MajorDuties { get; set; } = [];

    /// <summary>Gets or sets the education requirements text.</summary>
    [JsonPropertyName("Education")]
    public string? Education { get; set; }

    /// <summary>Gets or sets the conditions of employment text.</summary>
    [JsonPropertyName("Requirements")]
    public string? Requirements { get; set; }

    /// <summary>Gets or sets the evaluation / rating criteria text.</summary>
    [JsonPropertyName("Evaluations")]
    public string? Evaluations { get; set; }

    /// <summary>Gets or sets the how-to-apply instructions text.</summary>
    [JsonPropertyName("HowToApply")]
    public string? HowToApply { get; set; }

    /// <summary>Gets or sets the what-to-expect-next text.</summary>
    [JsonPropertyName("WhatToExpectNext")]
    public string? WhatToExpectNext { get; set; }

    /// <summary>Gets or sets the required documents text.</summary>
    [JsonPropertyName("RequiredDocuments")]
    public string? RequiredDocuments { get; set; }

    /// <summary>Gets or sets the benefits description text.</summary>
    [JsonPropertyName("Benefits")]
    public string? Benefits { get; set; }

    /// <summary>Gets or sets the URL linking to the benefits information page.</summary>
    [JsonPropertyName("BenefitsUrl")]
    public string? BenefitsUrl { get; set; }

    /// <summary>Gets or sets a value indicating whether the default benefits text should be displayed.</summary>
    [JsonPropertyName("BenefitsDisplayDefaultText")]
    public bool BenefitsDisplayDefaultText { get; set; }

    /// <summary>Gets or sets additional information text.</summary>
    [JsonPropertyName("OtherInformation")]
    public string? OtherInformation { get; set; }

    /// <summary>Gets or sets the list of key requirements for this position.</summary>
    [JsonPropertyName("KeyRequirements")]
    public List<string> KeyRequirements { get; set; } = [];

    /// <summary>Gets or sets a string indicating whether this result is within the search area ("True"/"False").</summary>
    [JsonPropertyName("WithinArea")]
    public string? WithinArea { get; set; }

    /// <summary>Gets or sets the commute distance used in any radial search.</summary>
    [JsonPropertyName("CommuteDistance")]
    public string? CommuteDistance { get; set; }

    /// <summary>Gets or sets the service type code for this position (e.g. "01").</summary>
    [JsonPropertyName("ServiceType")]
    public string? ServiceType { get; set; }

    /// <summary>Gets or sets the announcement closing type code.</summary>
    [JsonPropertyName("AnnouncementClosingType")]
    public string? AnnouncementClosingType { get; set; }

    /// <summary>Gets or sets the announcement closing type option value.</summary>
    [JsonPropertyName("AnnouncementClosingTypeOption")]
    public string? AnnouncementClosingTypeOption { get; set; }

    /// <summary>Gets or sets the agency contact e-mail address.</summary>
    [JsonPropertyName("AgencyContactEmail")]
    public string? AgencyContactEmail { get; set; }

    /// <summary>Gets or sets the agency contact phone number.</summary>
    [JsonPropertyName("AgencyContactPhone")]
    public string? AgencyContactPhone { get; set; }

    /// <summary>Gets or sets the agency contact website URL.</summary>
    [JsonPropertyName("AgencyContactWebsite")]
    public string? AgencyContactWebsite { get; set; }

    /// <summary>Gets or sets the required security clearance level (e.g. "Secret").</summary>
    [JsonPropertyName("SecurityClearance")]
    public string? SecurityClearance { get; set; }

    /// <summary>Gets or sets a string indicating whether a drug test is required ("True"/"False").</summary>
    [JsonPropertyName("DrugTestRequired")]
    public string? DrugTestRequired { get; set; }

    /// <summary>Gets or sets the position sensitivity / risk level description.</summary>
    [JsonPropertyName("PositionSensitivitiy")]
    public string? PositionSensitivity { get; set; }

    /// <summary>Gets or sets the adjudication type codes required for this position.</summary>
    [JsonPropertyName("AdjudicationType")]
    public List<string> AdjudicationType { get; set; } = [];

    /// <summary>Gets or sets a value indicating whether telework is eligible for this position.</summary>
    [JsonPropertyName("TeleworkEligible")]
    public bool TeleworkEligible { get; set; }

    /// <summary>Gets or sets a value indicating whether this is a fully remote position.</summary>
    [JsonPropertyName("RemoteIndicator")]
    public bool RemoteIndicator { get; set; }

    /// <summary>Gets or sets a value indicating whether financial disclosure is required.</summary>
    [JsonPropertyName("FinancialDisclosure")]
    public bool FinancialDisclosure { get; set; }

    /// <summary>Gets or sets a value indicating whether this position is in a bargaining unit.</summary>
    [JsonPropertyName("BargainingUnitStatus")]
    public bool BargainingUnitStatus { get; set; }

    /// <summary>Gets or sets the URL for a second/companion announcement, if applicable.</summary>
    [JsonPropertyName("SecondAnnouncementUrl")]
    public string? SecondAnnouncementUrl { get; set; }
}
