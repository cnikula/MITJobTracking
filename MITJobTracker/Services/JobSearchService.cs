// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-10-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-10-2026
// ***********************************************************************
// <copyright file="JobSearchService.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Service for calling the JSearch external job search API via RapidAPI</summary>
// ***********************************************************************

using System.Text.Json;
using MITJobTracker.Data.Models.JobSearch;
using MITJobTracker.Services.Interfaces;

namespace MITJobTracker.Services;

public class JobSearchService : IJobSearchService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public JobSearchService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<JobSearchResponse?> SearchJobsAsync(JobSearchRequest request)
    {
        var client = _httpClientFactory.CreateClient("JSearch");

        // Build query string from populated request fields
        var queryParams = new List<string>
        {
            $"query={Uri.EscapeDataString(request.Query)}",
            $"page={request.Page}",
            $"num_pages={request.NumPages}"
        };

        if (!string.IsNullOrWhiteSpace(request.Country))
            queryParams.Add($"country={Uri.EscapeDataString(request.Country)}");

        if (!string.IsNullOrWhiteSpace(request.Language))
            queryParams.Add($"language={Uri.EscapeDataString(request.Language)}");

        if (!string.IsNullOrWhiteSpace(request.Location))
            queryParams.Add($"location={Uri.EscapeDataString(request.Location)}");

        if (request.DatePosted != "all")
            queryParams.Add($"date_posted={request.DatePosted}");

        if (request.WorkFromHome)
            queryParams.Add("work_from_home=true");

        if (request.EmploymentTypes.Count > 0)
            queryParams.Add($"employment_types={string.Join(",", request.EmploymentTypes)}");

        if (request.JobRequirements.Count > 0)
            queryParams.Add($"job_requirements={string.Join(",", request.JobRequirements)}");

        if (request.Radius.HasValue && request.Radius > 0)
            queryParams.Add($"radius={request.Radius}");

        if (!string.IsNullOrWhiteSpace(request.ExcludeJobPublishers))
            queryParams.Add($"exclude_job_publishers={Uri.EscapeDataString(request.ExcludeJobPublishers)}");

        var url = $"search?{string.Join("&", queryParams)}";

        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
        httpRequest.Headers.Add("x-rapidapi-key", _configuration["RapidApi:JSearchKey"]);
        httpRequest.Headers.Add("x-rapidapi-host", _configuration["RapidApi:JSearchHost"]);

        using var response = await client.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<JobSearchResponse>(json, _jsonOptions);
    }
}