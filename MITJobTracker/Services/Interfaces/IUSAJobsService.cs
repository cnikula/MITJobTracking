// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 05-01-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 05-01-2026
// ***********************************************************************
// <copyright file="IUSAJobsService.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Interface for the USAJOBS API service</summary>
// ***********************************************************************

using MITJobTracker.Data.Models.JobSearch;

namespace MITJobTracker.Services.Interfaces;

public interface IUSAJobsService
{
    /// <summary>Search jobs by keyword (endpoint 1).</summary>
    Task<USAJobsSearchResponse?> SearchByKeywordAsync(string keyword);

    /// <summary>Search jobs by position title (endpoint 2).</summary>
    Task<USAJobsSearchResponse?> SearchByPositionTitleAsync(string positionTitle);

    /// <summary>Retrieve the agency sub-elements code list (endpoint 3).</summary>
    Task<USAJOBSCodeListResponse?> GetAgencySubElementsAsync();

    /// <summary>Search jobs by location city and state (endpoint 4).</summary>
    Task<USAJobsSearchResponse?> SearchByLocationAsync(string locationName);
}
