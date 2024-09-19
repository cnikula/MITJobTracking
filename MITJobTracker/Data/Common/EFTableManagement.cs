
using Microsoft.EntityFrameworkCore;
using MITJobTracker.Data.DTOS;


namespace MITJobTracker.Data.Common
{
    public class EFTableManagement
    {
        private readonly AppDBContext _context;

        public EFTableManagement(AppDBContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Updates job and interview by Id.
        /// </summary>
        /// <param name="JobsInterview">The jobs & interview DTO.</param>
        /// <returns>Boolean.</returns>
        public async Task<Boolean> UpdateJob_InterviwById(JobsInterviewDTO JobsInterview)
        {
            bool returnValue = false;

            try
            {
                // Retrieve the existing Job entity
                Job jobEntity = await _context.Jobs.FindAsync(JobsInterview.JobId);

                if (jobEntity != null)
                {
                    // Update the job entity with the new values
                    jobEntity.JobTitle = JobsInterview.JobTitle;
                    jobEntity.JobNo = JobsInterview.JobNo;
                    jobEntity.DateApplied = JobsInterview.DateApplied;
                    jobEntity.ResumeSendDate = JobsInterview.ResumeSendDate;
                    jobEntity.JobLocation = JobsInterview.JobLocation;
                    jobEntity.Duration = JobsInterview.Duration;
                    jobEntity.Salary = JobsInterview.Salary;
                    jobEntity.Remote = JobsInterview.Remote;
                    jobEntity.Hybrid = JobsInterview.Hybrid;
                    jobEntity.OnSite = JobsInterview.OnSite;
                    jobEntity.HybridNoOfDays = JobsInterview.HybridNoOfDays;
                    jobEntity.CompanyName = JobsInterview.CompanyName;
                    jobEntity.RecruitingAgency = JobsInterview.RecruitingAgency;
                    jobEntity.EmploymentType = JobsInterview.EmploymentType;
                    jobEntity.SubContract = JobsInterview.SubContract;
                    jobEntity.ResumeSend = JobsInterview.ResumeSend;
                    jobEntity.Requirements = JobsInterview.Requirements;
                    jobEntity.JobDescription = JobsInterview.JobDescription;
                    jobEntity.RecruitertName = JobsInterview.RecruitertName;
                    jobEntity.RecruiterPhone = JobsInterview.RecruiterPhone;
                    jobEntity.RecruiterEmail = JobsInterview.RecruiterEmail;

                    // Mark the job entity as modified
                    _context.Entry(jobEntity).State = EntityState.Modified;
                }

                // Retrieve the existing Interview entity
                Interview InterviewEntity = await _context.Interviews.FindAsync(JobsInterview.InterviewId);

                if (InterviewEntity != null)
                {
                    // Update the interview entity with the new values
                    InterviewEntity.JobId = JobsInterview.JobId;
                    InterviewEntity.InterviewDate = JobsInterview.InterviewDate;
                    InterviewEntity.InterviewType = JobsInterview.InterviewType;
                    InterviewEntity.InterviewerName = JobsInterview.InterviewerName;
                    InterviewEntity.InterviewerPhone = JobsInterview.InterviewerPhone;
                    InterviewEntity.InterviewerEmail = JobsInterview.InterviewerEmail;
                    InterviewEntity.InterviewerNotes = JobsInterview.InterviewerNotes;
                    InterviewEntity.InterviewerResulte = JobsInterview.InterviewerResulte;

                    // Mark the interview entity as modified
                    _context.Entry(InterviewEntity).State = EntityState.Modified;
                }

                // Save changes to the database
                await _context.SaveChangesAsync();
                returnValue = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Message: {e.Message}, Error No. {e.HResult}");
                returnValue = false;
            }

            return returnValue;
        }




        //// <summary>
        /// Updates the job and add interview by Id.
        /// </summary>
        /// <param name="JobsInterview">The jobs interview.</param>
        /// <returns>System.Threading.Tasks.Task&lt;System.Boolean&gt;.</returns>
        public async Task<Boolean> UpdateJob_AddInterviwById(JobsInterviewDTO JobsInterview)
        {
            bool returnValue = false;

            try
            {
                // Retrieve the existing Job entity
                Job jobEntity = await _context.Jobs.FindAsync(JobsInterview.JobId);

                if (jobEntity != null)
                {
                    // Update the job entity with the new values
                    jobEntity.JobTitle = JobsInterview.JobTitle;
                    jobEntity.JobNo = JobsInterview.JobNo;
                    jobEntity.DateApplied = JobsInterview.DateApplied;
                    jobEntity.ResumeSendDate = JobsInterview.ResumeSendDate;
                    jobEntity.JobLocation = JobsInterview.JobLocation;
                    jobEntity.Duration = JobsInterview.Duration;
                    jobEntity.Salary = JobsInterview.Salary;
                    jobEntity.Remote = JobsInterview.Remote;
                    jobEntity.Hybrid = JobsInterview.Hybrid;
                    jobEntity.OnSite = JobsInterview.OnSite;
                    jobEntity.HybridNoOfDays = JobsInterview.HybridNoOfDays;
                    jobEntity.CompanyName = JobsInterview.CompanyName;
                    jobEntity.RecruitingAgency = JobsInterview.RecruitingAgency;
                    jobEntity.EmploymentType = JobsInterview.EmploymentType;
                    jobEntity.SubContract = JobsInterview.SubContract;
                    jobEntity.ResumeSend = JobsInterview.ResumeSend;
                    jobEntity.Requirements = JobsInterview.Requirements;
                    jobEntity.JobDescription = JobsInterview.JobDescription;
                    jobEntity.RecruitertName = JobsInterview.RecruitertName;
                    jobEntity.RecruiterPhone = JobsInterview.RecruiterPhone;
                    jobEntity.RecruiterEmail = JobsInterview.RecruiterEmail;

                    // Mark the job entity as modified
                    _context.Entry(jobEntity).State = EntityState.Modified;
                }

                // Create a new Interview entity
                Interview InterviewEntity = new Interview
                {
                    JobId = JobsInterview.JobId,
                    InterviewDate = JobsInterview.InterviewDate,
                    InterviewType = JobsInterview.InterviewType,
                    InterviewerName = JobsInterview.InterviewerName,
                    InterviewerPhone = JobsInterview.InterviewerPhone,
                    InterviewerEmail = JobsInterview.InterviewerEmail,
                    InterviewerNotes = JobsInterview.InterviewerNotes,
                    InterviewerResulte = JobsInterview.InterviewerResulte
                };

                // Add the new Interview entity to the context
                _context.Interviews.Add(InterviewEntity);

                // Save changes to the database
                await _context.SaveChangesAsync();
                returnValue = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Message: {e.Message}, Error No. {e.HResult}");
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// Updates the job by Id.
        /// </summary>
        /// <param name="JobsInterview">The jobs interview.</param>
        /// <returns>Boolean.</returns>
        public async Task<Boolean> UpdateJob_ById(JobsInterviewDTO JobsInterview)
        {
            bool returnValue = false;

            try
            {
                // Retrieve the existing Job entity
                Job jobEntity = await _context.Jobs.FindAsync(JobsInterview.JobId);

                if (jobEntity != null)
                {
                    // Update the job entity with the new values
                    jobEntity.JobTitle = JobsInterview.JobTitle;
                    jobEntity.JobNo = JobsInterview.JobNo;
                    jobEntity.DateApplied = JobsInterview.DateApplied;
                    jobEntity.ResumeSendDate = JobsInterview.ResumeSendDate;
                    jobEntity.JobLocation = JobsInterview.JobLocation;
                    jobEntity.Duration = JobsInterview.Duration;
                    jobEntity.Salary = JobsInterview.Salary;
                    jobEntity.Remote = JobsInterview.Remote;
                    jobEntity.Hybrid = JobsInterview.Hybrid;
                    jobEntity.OnSite = JobsInterview.OnSite;
                    jobEntity.HybridNoOfDays = JobsInterview.HybridNoOfDays;
                    jobEntity.CompanyName = JobsInterview.CompanyName;
                    jobEntity.RecruitingAgency = JobsInterview.RecruitingAgency;
                    jobEntity.EmploymentType = JobsInterview.EmploymentType;
                    jobEntity.SubContract = JobsInterview.SubContract;
                    jobEntity.ResumeSend = JobsInterview.ResumeSend;
                    jobEntity.Requirements = JobsInterview.Requirements;
                    jobEntity.JobDescription = JobsInterview.JobDescription;
                    jobEntity.RecruitertName = JobsInterview.RecruitertName;
                    jobEntity.RecruiterPhone = JobsInterview.RecruiterPhone;
                    jobEntity.RecruiterEmail = JobsInterview.RecruiterEmail;

                    // Mark the job entity as modified
                    _context.Entry(jobEntity).State = EntityState.Modified;
                }

                // Save changes to the database
                await _context.SaveChangesAsync();
                returnValue = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Message: {e.Message}, Error No. {e.HResult}");
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// Add new Interview record to the Interview table by JobID.
        /// </summary>
        /// <param name="JobsInterview"></param>
        /// <returns></returns>
        public async Task<Boolean> InsertInterview(JobsInterviewDTO JobsInterview)
        {
            bool returnValue = false;

            try
            {
                // Create a new Interview entity
                Interview InterviewEntity = new Interview
                {
                    JobId = JobsInterview.JobId,
                    InterviewDate = JobsInterview.InterviewDate,
                    InterviewType = JobsInterview.InterviewType,
                    InterviewerName = JobsInterview.InterviewerName,
                    InterviewerPhone = JobsInterview.InterviewerPhone,
                    InterviewerEmail = JobsInterview.InterviewerEmail,
                    InterviewerNotes = JobsInterview.InterviewerNotes,
                    InterviewerResulte = JobsInterview.InterviewerResulte
                };

                // Add the new Interview entity to the context
                _context.Interviews.Add(InterviewEntity);

                // Save changes to the database
                await _context.SaveChangesAsync();
                returnValue = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Message: {e.Message}, Error No. {e.HResult}");
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// Update Interview table by ID
        /// </summary>
        /// <param name="JobsInterview"></param>
        /// <returns></returns>
        public async Task<Boolean> UpdateInterviewById(JobsInterviewDTO JobsInterview)
        {
            bool returnValue = false;

            try
            {
                // Retrieve the existing Interview entity
                Interview InterviewEntity = await _context.Interviews.FindAsync(JobsInterview.InterviewId);

                if (InterviewEntity != null)
                {
                    // Update the interview entity with the new values
                    InterviewEntity.JobId = JobsInterview.JobId;
                    InterviewEntity.InterviewDate = JobsInterview.InterviewDate;
                    InterviewEntity.InterviewType = JobsInterview.InterviewType;
                    InterviewEntity.InterviewerName = JobsInterview.InterviewerName;
                    InterviewEntity.InterviewerPhone = JobsInterview.InterviewerPhone;
                    InterviewEntity.InterviewerEmail = JobsInterview.InterviewerEmail;
                    InterviewEntity.InterviewerNotes = JobsInterview.InterviewerNotes;
                    InterviewEntity.InterviewerResulte = JobsInterview.InterviewerResulte;

                    // Mark the interview entity as modified
                    _context.Entry(InterviewEntity).State = EntityState.Modified;
                }

                // Save changes to the database
                await _context.SaveChangesAsync();
                returnValue = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Message: {e.Message}, Error No. {e.HResult}");
                returnValue = false;
            }

            return returnValue;
        }

        public async Task<int> DeleteJobById(int jobId)
        {
            int returnValue = 0;

            try
            {
                Interview interviewEntity = await _context.Interviews
                                    .Where(i => i.JobId == jobId)
                                    .FirstOrDefaultAsync();

                if (interviewEntity != null)
                {
                    // soft delete interview record
                    interviewEntity.IsDeleted = true;
                    interviewEntity.DeleteDate = DateTime.Now;
                    interviewEntity.DeleteByID= "CNikula";
                    interviewEntity.ModifiedById = "CNikula";
                    interviewEntity.ModifiedDate = DateTime.Now;

                    _context.Entry(interviewEntity).State = EntityState.Modified;
                }

                // Retrieve the existing Job entity
                Job jobEntity = await _context.Jobs.FindAsync(jobId);

                if (jobEntity != null)
                {
                    // soft delete job record
                    jobEntity.IsDeleted = true;
                    jobEntity.DeleteDate = DateTime.Now;
                    jobEntity.DeleteByID = "CNikula";
                    jobEntity.ModifiedById = "CNikula";
                    jobEntity.ModifiedDate = DateTime.Now;

                    _context.Entry(jobEntity).State = EntityState.Modified;
                }

                var countDeled =  await _context.SaveChangesAsync();
                returnValue = countDeled;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Message: {e.Message}, Error No. {e.HResult}");
                return 0;
            }

            return returnValue;
        }
    }

}
