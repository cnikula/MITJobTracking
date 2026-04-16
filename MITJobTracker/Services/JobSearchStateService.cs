// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-14-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-14-2026
// ***********************************************************************
// <copyright file="JobSearchStateService.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Scoped implementation of IJobSearchStateService.
//   No thread-safety is required because Scoped services in Blazor Server
//   are confined to a single circuit and accessed on the circuit's
//   synchronisation context.
// </summary>
// ***********************************************************************

using MITJobTracker.Data.Models.JobSearch;
using MITJobTracker.Services.Interfaces;

namespace MITJobTracker.Services;

public class JobSearchStateService : IJobSearchStateService
{
    public JobSearchRequest?  SearchRequest          { get; set; }
    public string[]           SelectedEmploymentTypes { get; set; } = [];
    public string[]           SelectedJobRequirements { get; set; } = [];
    public JobSearchResponse? LastResults             { get; set; }
    public DateTime?          LastSearchedAt          { get; set; }

    public bool HasResults => LastResults is not null;

    public void Clear()
    {
        SearchRequest           = null;
        SelectedEmploymentTypes = [];
        SelectedJobRequirements = [];
        LastResults             = null;
        LastSearchedAt          = null;
    }
}