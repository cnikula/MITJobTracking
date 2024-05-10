// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude
// Created          : 05-09-2024
//
// Last Modified By : Claude
// Last Modified On : 05-09-2024
// ***********************************************************************
// <copyright file="ProspectListDTO.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>DTO data class for Prospect List</summary>
// ***********************************************************************


namespace MITJobTracker.Data.DTOS
{
    public class ProspectListDTO
    {
            public int JobId { get; set; }
            public string JobTitle { get; set; }
            public string JobNo { get; set; }
            public DateTime DateApplied { get; set; }
            public string JobLocation { get; set; }
            public string RecruitingAgency { get; set; }
            public string RecruitertName { get; set; }
            public string RecruiterPhone { get; set; }
            public string RecruiterEmail { get; set; }
            public int InterviewId { get; set; }
            public DateTime InterviewDate { get; set; }
            public string InterviewType { get; set; }

    }
}
