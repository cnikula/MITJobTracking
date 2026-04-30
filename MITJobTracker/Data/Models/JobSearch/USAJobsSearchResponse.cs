// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-28-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-28-2026
// ***********************************************************************
// <copyright file="USAJobsSearchResponse.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Data models for the USAJOBS Job Search API (GET /api/Search) response.
//   Designed for lightweight JOA content consumption as delivered to commercial
//   job boards, mobile applications, and social media sites.
// </summary>
// ***********************************************************************

using System.Text.Json.Serialization;

namespace MITJobTracker.Data.Models.JobSearch;

public class USAJobsSearchResponse
{
    [JsonPropertyName("LanguageCode")]
    public string LanguageCode { get; set; } = string.Empty;

    [JsonPropertyName("SearchParameters")]
    public object SearchParameters { get; set; } = new();

    [JsonPropertyName("SearchResult")]
    public USAJobsSearchResultResponse SearchResult { get; set; } = new();
}

public class USAJobsSearchResultResponse
{
    [JsonPropertyName("SearchResultCount")]
    public int SearchResultCount { get; set; }

    [JsonPropertyName("SearchResultCountAll")]
    public int SearchResultCountAll { get; set; }

    [JsonPropertyName("SearchResultItems")]
    public List<USAJobsSearchResultItemResponse> SearchResultItems { get; set; } = [];
}

public class USAJobsSearchResultItemResponse
{
    [JsonPropertyName("MatchedObjectId")]
    public string MatchedObjectId { get; set; } = string.Empty;

    [JsonPropertyName("MatchedObjectDescriptor")]
    public USAJobsPositionDescriptorResponse MatchedObjectDescriptor { get; set; } = new();

    [JsonPropertyName("RelevanceRank")]
    public int RelevanceRank { get; set; }
}

public class USAJobsPositionDescriptorResponse
{
    [JsonPropertyName("PositionID")]
    public string PositionId { get; set; } = string.Empty;

    [JsonPropertyName("PositionTitle")]
    public string PositionTitle { get; set; } = string.Empty;

    [JsonPropertyName("PositionURI")]
    public string PositionUri { get; set; } = string.Empty;

    [JsonPropertyName("ApplyURI")]
    public List<string> ApplyUri { get; set; } = [];

    [JsonPropertyName("PositionLocationDisplay")]
    public string PositionLocationDisplay { get; set; } = string.Empty;

    [JsonPropertyName("PositionLocation")]
    public List<USAJobsPositionLocationResponse> PositionLocation { get; set; } = [];

    [JsonPropertyName("OrganizationName")]
    public string OrganizationName { get; set; } = string.Empty;

    [JsonPropertyName("DepartmentName")]
    public string DepartmentName { get; set; } = string.Empty;

    // Nullable: not present on all listings
    [JsonPropertyName("SubAgency")]
    public string? SubAgency { get; set; }

    [JsonPropertyName("JobCategory")]
    public List<USAJobsCodeNameResponse> JobCategory { get; set; } = [];

    [JsonPropertyName("JobGrade")]
    public List<USAJobsCode> JobGrade { get; set; } = [];

    [JsonPropertyName("PositionSchedule")]
    public List<USAJobsCodeNameResponse> PositionSchedule { get; set; } = [];

    [JsonPropertyName("PositionOfferingType")]
    public List<USAJobsCodeNameResponse> PositionOfferingType { get; set; } = [];

    [JsonPropertyName("QualificationSummary")]
    public string QualificationSummary { get; set; } = string.Empty;

    [JsonPropertyName("PositionRemuneration")]
    public List<USAJobsPositionRemunerationResponse> PositionRemuneration { get; set; } = [];

    [JsonPropertyName("PositionStartDate")]
    public DateTime? PositionStartDate { get; set; }

    [JsonPropertyName("PositionEndDate")]
    public DateTime? PositionEndDate { get; set; }

    [JsonPropertyName("PublicationStartDate")]
    public DateTime? PublicationStartDate { get; set; }

    [JsonPropertyName("ApplicationCloseDate")]
    public DateTime? ApplicationCloseDate { get; set; }

    [JsonPropertyName("PositionFormattedDescription")]
    public List<USAJobsFormattedDescriptionResponse> PositionFormattedDescription { get; set; } = [];

    [JsonPropertyName("UserArea")]
    public USAJobsUserAreaResponse UserArea { get; set; } = new();
}

public class USAJobsPositionLocationResponse
{
    [JsonPropertyName("LocationName")]
    public string LocationName { get; set; } = string.Empty;

    [JsonPropertyName("CountryCode")]
    public string CountryCode { get; set; } = string.Empty;

    // Nullable: not present for non-US locations
    [JsonPropertyName("CountrySubDivisionCode")]
    public string? CountrySubDivisionCode { get; set; }

    [JsonPropertyName("CityName")]
    public string CityName { get; set; } = string.Empty;

    [JsonPropertyName("Longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("Latitude")]
    public double Latitude { get; set; }
}

public class USAJobsCodeNameResponse
{
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("Code")]
    public string Code { get; set; } = string.Empty;
}

public class USAJobsCode
{
    [JsonPropertyName("Code")]
    public string Code { get; set; } = string.Empty;
}

public class USAJobsPositionRemunerationResponse
{
    [JsonPropertyName("MinimumRange")]
    public string MinimumRange { get; set; } = string.Empty;

    [JsonPropertyName("MaximumRange")]
    public string MaximumRange { get; set; } = string.Empty;

    [JsonPropertyName("RateIntervalCode")]
    public string RateIntervalCode { get; set; } = string.Empty;

    [JsonPropertyName("Description")]
    public string Description { get; set; } = string.Empty;
}

public class USAJobsFormattedDescriptionResponse
{
    [JsonPropertyName("Label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("LabelDescription")]
    public string LabelDescription { get; set; } = string.Empty;
}

public class USAJobsUserAreaResponse
{
    [JsonPropertyName("Details")]
    public USAJobsDetailsResponse Details { get; set; } = new();

    [JsonPropertyName("IsRadialSearch")]
    public bool IsRadialSearch { get; set; }
}

public class USAJobsDetailsResponse
{
    [JsonPropertyName("JobSummary")]
    public string JobSummary { get; set; } = string.Empty;

    [JsonPropertyName("WhoMayApply")]
    public USAJobsCodeNameResponse WhoMayApply { get; set; } = new();

    [JsonPropertyName("LowGrade")]
    public string LowGrade { get; set; } = string.Empty;

    [JsonPropertyName("HighGrade")]
    public string HighGrade { get; set; } = string.Empty;

    [JsonPropertyName("PromotionPotential")]
    public string PromotionPotential { get; set; } = string.Empty;

    // Nullable: not present on all listings
    [JsonPropertyName("PromotionPotentialAdditionalText")]
    public string? PromotionPotentialAdditionalText { get; set; }

    // Nullable: not present on all listings
    [JsonPropertyName("SubAgencyName")]
    public string? SubAgencyName { get; set; }

    [JsonPropertyName("OrganizationCodes")]
    public string OrganizationCodes { get; set; } = string.Empty;

    [JsonPropertyName("Relocation")]
    public string Relocation { get; set; } = string.Empty;

    [JsonPropertyName("HiringPath")]
    public List<string> HiringPath { get; set; } = [];

    [JsonPropertyName("MCOTags")]
    public List<string> MCOTags { get; set; } = [];

    [JsonPropertyName("TotalOpenings")]
    public string TotalOpenings { get; set; } = string.Empty;

    [JsonPropertyName("AgencyMarketingStatement")]
    public string AgencyMarketingStatement { get; set; } = string.Empty;

    [JsonPropertyName("TravelCode")]
    public string TravelCode { get; set; } = string.Empty;

    // Nullable: not present on all listings
    [JsonPropertyName("ApplyOnlineUrl")]
    public string? ApplyOnlineUrl { get; set; }

    // Nullable: not present on all listings
    [JsonPropertyName("DetailStatusUrl")]
    public string? DetailStatusUrl { get; set; }

    [JsonPropertyName("MajorDuties")]
    public List<string> MajorDuties { get; set; } = [];

    [JsonPropertyName("Education")]
    public string Education { get; set; } = string.Empty;

    [JsonPropertyName("Requirements")]
    public string Requirements { get; set; } = string.Empty;

    [JsonPropertyName("Evaluations")]
    public string Evaluations { get; set; } = string.Empty;

    [JsonPropertyName("HowToApply")]
    public string HowToApply { get; set; } = string.Empty;

    [JsonPropertyName("WhatToExpectNext")]
    public string WhatToExpectNext { get; set; } = string.Empty;

    [JsonPropertyName("RequiredDocuments")]
    public string RequiredDocuments { get; set; } = string.Empty;

    [JsonPropertyName("Benefits")]
    public string Benefits { get; set; } = string.Empty;

    // Nullable: not present on all listings
    [JsonPropertyName("BenefitsUrl")]
    public string? BenefitsUrl { get; set; }

    [JsonPropertyName("BenefitsDisplayDefaultText")]
    public bool BenefitsDisplayDefaultText { get; set; }

    [JsonPropertyName("OtherInformation")]
    public string OtherInformation { get; set; } = string.Empty;

    [JsonPropertyName("KeyRequirements")]
    public List<string> KeyRequirements { get; set; } = [];

    [JsonPropertyName("WithinArea")]
    public string WithinArea { get; set; } = string.Empty;

    [JsonPropertyName("CommuteDistance")]
    public string CommuteDistance { get; set; } = string.Empty;

    // Nullable: not present on all listings
    [JsonPropertyName("SecondAnnouncementUrl")]
    public string? SecondAnnouncementUrl { get; set; }

    [JsonPropertyName("ServiceType")]
    public string ServiceType { get; set; } = string.Empty;

    [JsonPropertyName("AnnouncementClosingType")]
    public string AnnouncementClosingType { get; set; } = string.Empty;

    // Nullable: not present on all listings
    [JsonPropertyName("AnnouncementClosingTypeOption")]
    public string? AnnouncementClosingTypeOption { get; set; }

    // Nullable: not present on all listings
    [JsonPropertyName("AgencyContactEmail")]
    public string? AgencyContactEmail { get; set; }

    // Nullable: not present on all listings
    [JsonPropertyName("AgencyContactPhone")]
    public string? AgencyContactPhone { get; set; }

    // Nullable: not present on all listings
    [JsonPropertyName("AgencyContactWebsite")]
    public string? AgencyContactWebsite { get; set; }

    [JsonPropertyName("SecurityClearance")]
    public string SecurityClearance { get; set; } = string.Empty;

    [JsonPropertyName("DrugTestRequired")]
    public string DrugTestRequired { get; set; } = string.Empty;

    // Nullable: not present on all listings
    [JsonPropertyName("PositionSensitivitiy")]
    public string? PositionSensitivity { get; set; }

    [JsonPropertyName("AdjudicationType")]
    public List<string> AdjudicationType { get; set; } = [];

    [JsonPropertyName("TeleworkEligible")]
    public bool TeleworkEligible { get; set; }

    [JsonPropertyName("RemoteIndicator")]
    public bool RemoteIndicator { get; set; }

    [JsonPropertyName("FinancialDisclosure")]
    public bool FinancialDisclosure { get; set; }

    [JsonPropertyName("BargainingUnitStatus")]
    public bool BargainingUnitStatus { get; set; }
}
