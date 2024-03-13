using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using MITJobTracker.Data.Common;

namespace MITJobTracker.Data
{
    public class Interview : CommonModel
    {
        [Comment("InterviewId, Table primary key")]
        [Key]
        [DataMember]
        public int InterviewId { get; set; }

        [Comment("JobId, foreign key to the Jobs table")]
        [DataMember]
        [Required(ErrorMessage = "JobId is required")]
        public int JobId { get; set; }

        [Comment("Interview Date, the date of the interview")]
        [DataMember]
        [Required(ErrorMessage = "Interview Date is required")]
        public DateTime InterviewDate { get; set; }

        [Comment("Interview Type, is interview by phone, in-person or via computer. Example Zoome, Teams, Sky ")]
        [DataMember]
        [MaxLength(150)]
        [Required(ErrorMessage = "Interview Type is required")]
        public string InterviewType { get; set; }

        [Comment("Interviewer Name, the person full name that is interviewing you")]
        [DataMember]
        [MaxLength(150)]
        [Required(ErrorMessage = "Interviewer Name is required")]
        public string InterviewerName { get; set; }

        [Comment("Interviewer Phone, the person phone number that is interviewing you")]
        [DataMember]
        [MaxLength(15)]
        public string? InterviewerPhone { get; set; }

        [Comment("Interviewer Email, the person email that is interviewing you")]
        [DataMember]
        [MaxLength(150)]
        public string? InterviewerEmail { get; set; }

        [Comment("Interviewer Notes, any notes that you want to keep about the interview")]
        [DataMember]
        [MaxLength(500)]
        public string? InterviewerNotes { get; set; }

        [Comment("Interviewer Result, the result of the interview")]
        [DataMember]
        [MaxLength(250)]
        public string? InterviewerResulte { get; set; }


        //public virtual Job Job { get; set; }
    }
}
