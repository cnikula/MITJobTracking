// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-14-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-14-2026
// ***********************************************************************
// <copyright file="IJobSearchStateService.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Scoped state container for the Job Search page.
//   Survives Blazor navigation within the same circuit (browser tab)
//   so the user's search criteria and results are preserved without
//   making a second API call.
// </summary>
// ***********************************************************************

using MITJobTracker.Data.Models.JobSearch;

namespace MITJobTracker.Services.Interfaces;

public interface IJobSearchStateService
{
    /// <summary>The last submitted search request parameters.</summary>
    JobSearchRequest? SearchRequest { get; set; }

    /// <summary>Selected employment type tokens from the multi-select.</summary>
    string[] SelectedEmploymentTypes { get; set; }

    /// <summary>Selected job requirement tokens from the multi-select.</summary>
    string[] SelectedJobRequirements { get; set; }

    /// <summary>The results returned by the last successful API call.</summary>
    JobSearchResponse? LastResults { get; set; }

    /// <summary>UTC timestamp of the last successful search (null = never searched).</summary>
    DateTime? LastSearchedAt { get; set; }

    /// <summary>True when cached results are available to restore.</summary>
    bool HasResults { get; }

    /// <summary>Clears all saved state and resets to defaults.</summary>
    void Clear();
}