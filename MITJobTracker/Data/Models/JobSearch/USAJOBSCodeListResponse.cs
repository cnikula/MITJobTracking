// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 04-28-2026
//
// Last Modified By : Claude Nikula
// Last Modified On : 04-28-2026
// ***********************************************************************
// <copyright file="USAJOBSCodeList.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
//   Represents the root code list response returned by the USAJOBS code list API.
// </summary>
// ***********************************************************************

using System.Text.Json.Serialization;

namespace MITJobTracker.Data.Models.JobSearch;

/// <summary>
/// Represents the root response object returned by the USAJOBS code list API.
/// </summary>
public sealed class USAJOBSCodeListResponse
{
    /// <summary>Gets or sets the collection of code list groups returned by the API.</summary>
    [JsonPropertyName("CodeList")]
    public List<USAJOBSCodeListGroupResponse> CodeList { get; set; } = [];

    /// <summary>Gets or sets the UTC date and time at which this code list was generated.</summary>
    [JsonPropertyName("DateGenerated")]
    public DateTimeOffset DateGenerated { get; set; }
}

/// <summary>
/// Represents a single named group of valid values within the USAJOBS code list.
/// </summary>
public sealed class USAJOBSCodeListGroupResponse
{
    /// <summary>Gets or sets the collection of valid code/value pairs in this group.</summary>
    [JsonPropertyName("ValidValue")]
    public List<USAJOBSValidValueResponse> ValidValue { get; set; } = [];

    /// <summary>Gets or sets the identifier for this code list group (e.g. "AgencySubElement").</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

/// <summary>
/// Represents a single valid code/value entry within a USAJOBS code list group.
/// </summary>
public sealed class USAJOBSValidValueResponse
{
    /// <summary>Gets or sets the short code identifier (e.g. "AF00", "ARAT").</summary>
    [JsonPropertyName("Code")]
    public string Code { get; set; } = string.Empty;

    /// <summary>Gets or sets the human-readable name for this code (e.g. "Department of the Air Force - Agency Wide").</summary>
    [JsonPropertyName("Value")]
    public string Value { get; set; } = string.Empty;

    /// <summary>Gets or sets the parent code for hierarchical grouping; may be <see langword="null"/> for top-level entries.</summary>
    [JsonPropertyName("ParentCode")]
    public string? ParentCode { get; set; }

    /// <summary>Gets or sets the commonly known acronym for this entity (e.g. "USDA", "DOD"); may be empty.</summary>
    [JsonPropertyName("Acronym")]
    public string? Acronym { get; set; }

    /// <summary>Gets or sets the ISO 8601 date/time string indicating when this entry was last modified.</summary>
    [JsonPropertyName("LastModified")]
    public string? LastModified { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this code is disabled.
    /// The API returns <c>"Yes"</c> or <c>"No"</c> as a string.
    /// </summary>
    [JsonPropertyName("IsDisabled")]
    public string? IsDisabled { get; set; }

    /// <summary>
    /// Gets a boolean indicating whether this entry is disabled,
    /// derived from the <see cref="IsDisabled"/> string value.
    /// </summary>
    [JsonIgnore]
    public bool Disabled =>
        string.Equals(IsDisabled, "Yes", StringComparison.OrdinalIgnoreCase);
}
