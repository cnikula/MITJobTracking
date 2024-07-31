// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : Claude Nikula
// Created          : 07-16-2024
//
// Last Modified By : Claude Nikula
// Last Modified On : 07-16-2024
// ***********************************************************************
// <copyright file="JobsInterviewDTO.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>DTO Class merging Jobs and Interview fields</summary>
// ***********************************************************************


namespace MITJobTracker.Data.DTOS
{
    public class JobsInterviewDTO
    {

            public int JobId { get; set; }
            public string JobTitle { get; set; }
            public string JobNo { get; set; }
            public string CompanyName { get; set; }
            public string RecruitingAgency { get; set; }
            public string RecruitertName { get; set; }
            public string RecruiterPhone { get; set; }
            public string RecruiterEmail { get; set; }
            public string JobDescription { get; set; }
            public string JobLocation { get; set; }
            public bool Remote { get; set; }
            public bool Hybrid { get; set; }
            public string HybridNoOfDays { get; set; }
            public string Requirements { get; set; }
            public string Salary { get; set; }
            public string EmploymentType { get; set; }
            public bool SubContract { get; set; }
            public bool ResumeSend { get; set; }
            public DateTime ResumeSendDate { get; set; }
            public DateTime DateApplied { get; set; }
            public string Duration { get; set; }
            public bool OnSite { get; set; }
            public int InterviewId { get; set; }
            public DateTime InterviewDate { get; set; }
            public string InterviewType { get; set; }
            public string InterviewerName { get; set; }
            public string InterviewerPhone { get; set; }
            public string InterviewerEmail { get; set; }
            public string InterviewerNotes { get; set; }
            public string InterviewerResulte { get; set; }
        

    }
}
