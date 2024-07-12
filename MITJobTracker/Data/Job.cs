// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : techn
// Created          : 03-13-2024
//
// Last Modified By : techn
// Last Modified On : 04-24-2024
// ***********************************************************************
// <copyright file="Job.cs" company="MITJobTracker">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>POKO Data class</summary>
// ***********************************************************************


using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using MITJobTracker.Data.Common;

namespace MITJobTracker.Data
{
    public class Job : CommonModel
    {
        public Job()
        {
            Remote = false;
            Hybrid = false;
            SubContract = false;
            ResumeSend = false;
            OnSite = false;
            DateApplied = DateTime.Now;
            JobNo = "0";
            Salary = "0";
            Duration = "N/A";
        }

        [Comment("Table primary key")]
        [DataMember]
        [Key]
        public int JobId { get; set; }

        [Comment("Job Title, description of the title")]
        [DataMember]
        [MaxLength(75)]
        [Required(ErrorMessage = "Job Title is required")]
        public string JobTitle { get; set; }

        [Comment("Job Number, position identifier for the job you are appalling for.")]
        [DataMember]
        [MaxLength(25)]
        public string? JobNo { get; set; }

        [Comment("Company Name, the company name you are applying for.")]
        [DataMember]
        [MaxLength(150)]
        public string? CompanyName { get; set; }

        [Comment("Recruiting Agency, the agency that is helping you to get the job. Note this can be the same as CompanyName ")]
        [DataMember]
        [MaxLength(150)]
        public string? RecruitingAgency { get; set; }

        [Comment("Recruiter Name, the agency person you are communicating with")]
        [DataMember]
        [MaxLength(150)]
        [Required(ErrorMessage = "Recruiter Name is required. If you do not know the name you enter UK")]
        public string RecruitertName { get; set; }

        [Comment("Recruiter Phone, the agency person you are communicating with phone number.")]
        [DataMember]
        [MaxLength(50)]
        public string? RecruiterPhone { get; set; }

        [Comment("Recruiter Email, the agency person you are communicating with email.")]
        [DataMember]
        [MaxLength(150)]
        public string? RecruiterEmail { get; set; }

        [Comment("Job Description, the job description.")]
        [DataMember]
        [Required(ErrorMessage = "Job Description is required")]
        public string JobDescription { get; set; }

        [Comment("Job Location, the job location.")]
        [DataMember]
        [MaxLength(150)]
        public string? JobLocation { get; set; }

        [Comment("Remote, if the job is remote.")]
        [DataMember]
        public bool Remote { get; set; }

        [Comment("Hybrid, if the job is hybrid.")]
        [DataMember]
        public bool Hybrid { get; set; }

        [Comment("OnSite, if the job is on site.")]
        [DataMember]
        public bool OnSite { get; set; }

        [Comment("Hybrid No Of Days, how may days on-site")]
        [DataMember]
        [MaxLength(25)]
        public string? HybridNoOfDays { get; set; }

        [Comment("Job Requirements, the job requirements.")]
        [DataMember]
        [Required(ErrorMessage = "Job Requirements is required")]
        public string Requirements { get; set; }

        [Comment("Salary, pay range per hour our salary")]
        [DataMember]
        [MaxLength(75)]
        public string? Salary { get; set; }

        [Comment("Employment Type, W2, 1099, Part-Time, Full-Time.")]
        [DataMember]
        [MaxLength(25)]
        [Required(ErrorMessage = "Employment Type is required")]
        public string EmploymentType { get; set; }

        [Comment("Duration, how long the job is for.")]
        [DataMember]
        [MaxLength(25)]
        public string? Duration { get; set; }

        [Comment("Sub-Contract, if you contract through another company. Example Head-hunter")]
        [DataMember]
        public bool SubContract { get; set; }

        [Comment("Resume Send, did you send a resume out")]
        [DataMember]
        public bool ResumeSend { get; set; }

        [Comment("Resume Send Date, the date you send the resume out or applied for the job. If resume send then ResumeSend is set to 1 (true)")]
        [DataMember]
        public DateTime? ResumeSendDate { get; set; }

        [Comment("DateApplied, the date you Applied for the job or send bid.")]
        [DataMember]
        public DateTime DateApplied { get; set; }

        //Table Relationship 
        public virtual ICollection<Interview> Interviews { get; set; }
    }
}
