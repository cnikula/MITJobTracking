// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-28-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-28-2026
// ***********************************************************************
// <copyright file="USAJOBSSearchByLocationCityAndState.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Data models for the USAJOBS Job Search API response when searching by
//   city and state location. Extends the base search response with a
//   resolved location in the top-level UserArea.
// </summary>
// ***********************************************************************

using System.Text.Json.Serialization;

namespace MITJobTracker.Data.Models.JobSearch;

public class USAJOBSSearchByLocationCityAndStateResponse
{
    [JsonPropertyName("LanguageCode")]
    public string LanguageCode { get; set; } = string.Empty;

    [JsonPropertyName("SearchParameters")]
    public object SearchParameters { get; set; } = new();

    [JsonPropertyName("SearchResult")]
    public USAJobsSearchByLocationResultResponse SearchResult { get; set; } = new();
}

public class USAJobsSearchByLocationResultResponse
{
    [JsonPropertyName("SearchResultCount")]
    public int SearchResultCount { get; set; }

    [JsonPropertyName("SearchResultCountAll")]
    public int SearchResultCountAll { get; set; }

    [JsonPropertyName("SearchResultItems")]
    public List<USAJobsSearchResultItemResponse> SearchResultItems { get; set; } = [];

    [JsonPropertyName("UserArea")]
    public USAJobsSearchByLocationUserAreaResponse UserArea { get; set; } = new();
}

public class USAJobsSearchByLocationUserAreaResponse
{
    [JsonPropertyName("NumberOfPages")]
    public string NumberOfPages { get; set; } = string.Empty;

    [JsonPropertyName("IsRadialSearch")]
    public bool IsRadialSearch { get; set; }

    [JsonPropertyName("ResolvedLocation")]
    public List<string> ResolvedLocation { get; set; } = [];
}
