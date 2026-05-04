// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 05-01-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 05-01-2026
// ***********************************************************************
// <copyright file="USAJobsService.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Service for calling the USAJOBS external API endpoints</summary>
// ***********************************************************************

using System.Text.Json;
using MITJobTracker.Data.Models.JobSearch;
using MITJobTracker.Services.Interfaces;

namespace MITJobTracker.Services;

public class USAJobsService : IUSAJobsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public USAJobsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    // ── Endpoint 1: Search by Keyword ─────────────────────────────────

    public async Task<USAJobsSearchResponse?> SearchByKeywordAsync(string keyword)
    {
        var url = $"api/search?Keyword={Uri.EscapeDataString(keyword)}";
        return await GetSearchResponseAsync(url, requiresFullHeaders: false);
    }

    // ── Endpoint 2: Search by Position Title ──────────────────────────

    public async Task<USAJobsSearchResponse?> SearchByPositionTitleAsync(string positionTitle)
    {
        var url = $"api/Search?PositionTitle={Uri.EscapeDataString(positionTitle)}";
        return await GetSearchResponseAsync(url, requiresFullHeaders: false);
    }

    // ── Endpoint 3: Agency Sub-Elements Code List ──────────────────────

    public async Task<USAJOBSCodeListResponse?> GetAgencySubElementsAsync()
    {
        var client = CreateClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, "api/codelist/agencysubelements");
        // Code list endpoint does not require auth headers
        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<USAJOBSCodeListResponse>(json, _jsonOptions);
    }

    // ── Endpoint 4: Search by Location ────────────────────────────────

    public async Task<USAJobsSearchResponse?> SearchByLocationAsync(string locationName)
    {
        var url = $"api/Search?LocationName={Uri.EscapeDataString(locationName)}";
        return await GetSearchResponseAsync(url, requiresFullHeaders: false);
    }

    // ── Shared helpers ─────────────────────────────────────────────────

    private async Task<USAJobsSearchResponse?> GetSearchResponseAsync(string url, bool requiresFullHeaders)
    {
        var client = CreateClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        AddAuthHeaders(request);

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<USAJobsSearchResponse>(json, _jsonOptions);
    }

    private HttpClient CreateClient() =>
        _httpClientFactory.CreateClient("USAJobs");

    private void AddAuthHeaders(HttpRequestMessage request)
    {
        var host = _configuration["USAJobs:Host"] ?? "data.usajobs.gov";
        var userAgent = _configuration["USAJobs:UserAgent"] ?? string.Empty;
        var authKey = _configuration["USAJobs:AuthorizationKey"] ?? string.Empty;

        request.Headers.TryAddWithoutValidation("Host", host);
        request.Headers.TryAddWithoutValidation("User-Agent", userAgent);
        request.Headers.TryAddWithoutValidation("Authorization-Key", authKey);
    }
}
