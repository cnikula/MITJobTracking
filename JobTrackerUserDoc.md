# How To Use JobTracker

**MITJobTracker User Guide — Version 10.28.1**

---

## Table of Contents

1. [Getting Started](#getting-started)
2. [Managing Job Applications](#managing-job-applications)
3. [Tracking Interviews](#tracking-interviews)
4. [Searching for Jobs (JSearch API)](#searching-for-jobs-jsearch-api)
5. [Searching Federal Jobs (USAJOBS API)](#searching-federal-jobs-usajobs-api)
6. [Using the Daily Job Search Log](#using-the-daily-job-search-log)
7. [Viewing Analytics](#viewing-analytics)
8. [Tips and Best Practices](#tips-and-best-practices)

---

## Getting Started

### Accessing the Application

MITJobTracker is deployed as a web application. Access it by navigating to:
```
http://your-server/mitJobTracker
```

### Home Page

The home page displays:
- The current application version
- Quick navigation to key features
- Overview of the application's capabilities

---

## Managing Job Applications

### Adding a New Job Application

1. Navigate to the **Job Prospect** page
2. Click the **Add Job** button
3. Fill in the required information:
   - **Job Title** (required) — The position title
   - **Recruiter Name** (required) — Primary contact person
   - **Company Name** (optional) — The hiring company
   - **Recruiting Agency** (optional) — Third-party agency, if applicable
   - **Recruiter Contact**:
	 - Phone number
	 - Email address
   - **Job Details**:
	 - Location
	 - Salary range
	 - Duration (for contract positions)
	 - Employment Type (Full-time, Part-time, Contract)
	 - Work Mode (Remote, Hybrid, On-Site)
	 - Hybrid Days Count (if Hybrid)
   - **Job Requirements** — Skills and qualifications needed
   - **Job Description** — Detailed description of the role
   - **Special Notes** — Any additional information
4. Set optional flags:
   - **SubContract** — Check if this is a subcontract position
   - **Resume Sent** — Check if you've already sent your resume
5. Click **Save**

**Note:** A unique Job Number (format: `JOB-YYYYMMDD-XXXX`) is automatically generated when you save.

### Viewing Job Applications

1. Navigate to the **View Prospect** page
2. The grid displays all your job applications with columns:
   - Job Number
   - Job Title
   - Company Name
   - Recruiter Name
   - Date Applied
   - Status
3. Use the grid features:
   - **Sort** — Click any column header to sort
   - **Search** — Use the search box to filter results
   - **Pagination** — Navigate through multiple pages if you have many applications

### Editing a Job Application

1. From the **View Prospect** grid, click on a job row
2. You'll be taken to the **Detail View** page
3. Modify any fields as needed
4. Click **Save Changes**

**Smart Save Feature:** The application only updates fields that have changed, improving performance.

### Deleting a Job Application

1. Navigate to the **View Prospect** page
2. Select the job(s) you want to delete using checkboxes
3. Click the **Delete** button
4. Confirm the deletion

**Note:** This is a soft delete — records are marked as deleted but retained in the database for audit purposes.

---

## Tracking Interviews

### Adding an Interview to a Job Application

1. Open a job application in **Detail View**
2. Scroll to the **Interviews** section
3. Click **Add Interview**
4. Enter interview details:
   - **Interview Date** — When the interview is scheduled
   - **Interview Type** — Phone, Video, or In-Person
   - **Interviewer Name** — Person conducting the interview
   - **Interviewer Contact**:
	 - Phone number
	 - Email address
   - **Interview Notes** — Preparation notes, questions to ask, etc.
   - **Interview Result** — Outcome (Pending, Passed, Failed, Scheduled for Next Round)
5. Click **Save**

### Editing an Interview

1. From the **Detail View** page, locate the interview in the interviews section
2. Click **Edit** next to the interview
3. Make your changes
4. Click **Save**

### Deleting an Interview

1. From the **Detail View** page, locate the interview
2. Click **Delete** next to the interview
3. Confirm the deletion

---

## Searching for Jobs (JSearch API)

The JSearch integration allows you to search live job postings from thousands of sources worldwide.

### Performing a Search

1. Navigate to the **Job Search** page
2. Configure your search criteria:
   - **Query** — Keywords (e.g., "software engineer", "data analyst")
   - **Location** — City, state, or country (e.g., "New York, NY")
   - **Date Posted** — All, Today, 3 Days, Week, Month
   - **Country** — Filter by specific country code
   - **Language** — Preferred job listing language
   - **Employment Type** (multi-select):
	 - Full-time
	 - Part-time
	 - Contract
	 - Internship
   - **Job Requirements** (multi-select):
	 - No experience required
	 - No degree required
	 - Under 3 years experience
   - **Remote Jobs Only** — Toggle for work-from-home positions
   - **Radius** — Search radius in miles/kilometers
   - **Exclude Publishers** — Comma-separated list of job boards to exclude
3. Click **Search**
4. Review results in the grid:
   - Job Title
   - Company Name
   - Location
   - Employment Type
   - Posted Date
   - Apply Link
5. Click **Apply** to open the job posting in a new tab

### Search State Preservation

Your search criteria and results are **automatically saved** while you navigate the application:
- Navigate away from the Job Search page
- When you return, your previous search is restored
- No additional API calls are consumed

This feature stays active for your entire browsing session.

### Starting a New Search

To clear your previous search and start fresh:
1. Modify your search criteria
2. Click **Search** again
3. The old results will be replaced with new ones

---

## Searching Federal Jobs (USAJOBS API)

The USAJOBS integration provides access to all U.S. federal government job openings.

### Search Modes

#### 1. Search by Keyword
1. Navigate to the **USAJOBS Search** page
2. Select **Keyword Search** mode
3. Enter keywords (e.g., "cybersecurity", "project manager")
4. Click **Search**

#### 2. Search by Position Title
1. Select **Position Title Search** mode
2. Enter the exact or partial position title (e.g., "Program Analyst")
3. Click **Search**

#### 3. Search by Location
1. Select **Location Search** mode
2. Enter city and state (e.g., "Washington, DC")
3. Click **Search**

#### 4. Agency Sub-Elements Code List
1. Select **Code List** mode
2. Click **Retrieve Codes**
3. View the complete list of federal agency codes
4. Use these codes to refine future searches

### Understanding USAJOBS Results

Each result includes:
- **Position Title** — Official job title
- **Department/Agency** — Federal agency hiring
- **Location(s)** — Where the position is based
- **Job Grade** — GS level and salary range
- **Opening/Closing Dates** — Application window
- **Announcement Number** — Unique identifier
- **Apply Link** — Direct link to USAJOBS application page

### Applying for Federal Jobs

1. Click the **Apply** link in the search results
2. You'll be redirected to the official USAJOBS listing
3. Create a USAJOBS account if you don't have one
4. Submit your application through the federal portal
5. Return to MITJobTracker and add the position as a job application to track it

---

## Using the Daily Job Search Log

The Daily Job Search Log helps you track which external job postings you've already reviewed today, preventing duplicate work.

### How It Works

**Automatic Tracking:**
- When you search using JSearch API, every job ID is logged
- The log is date-stamped for today
- Jobs you've already seen today can be filtered or flagged

### Viewing Today's Retrieved Jobs

1. Navigate to the **Job Search** page
2. Jobs retrieved earlier today may be marked with a "Previously Retrieved" indicator
3. This helps you focus on new opportunities

### Marking Jobs as Reviewed

1. After reviewing a job posting:
2. Click the **Mark as Reviewed** checkbox or button
3. The job is flagged with a `ReviewedAtUtc` timestamp
4. You can later filter to see only unreviewed jobs

### Resetting the Daily Log

If you want to start fresh for the day:
1. Click the **Reset Day** button on the Job Search page
2. Confirm the action
3. All of today's log entries are cleared
4. Your next search will treat all jobs as new

**Use Case:** You might reset if you're searching for different job types or locations during the same day and want to re-review certain postings.

---

## Viewing Analytics

The Analytics Dashboard provides insights into your job search progress.

### Accessing Analytics

1. Navigate to the **Analytics** or **Dashboard** page
2. Review the key metrics

### Key Metrics Displayed

#### 1. Total Applications
- **What it shows:** The total number of job applications you've submitted
- **How to use:** Track your overall activity level

#### 2. Active Applications
- **What it shows:** Applications still in progress (not rejected or closed)
- **How to use:** See how many opportunities you're currently pursuing

#### 3. Interview Rate
- **What it shows:** Percentage of applications that resulted in interviews
- **Formula:** (Number of interviews / Total applications) × 100
- **How to use:** Gauge the effectiveness of your applications

#### 4. Average Response Time
- **What it shows:** Average number of days between application and first response
- **How to use:** Set expectations for when you might hear back

### Visual Charts

The dashboard uses Syncfusion charts to visualize:
- Application trends over time
- Interview success rates
- Response time patterns
- Employment type distribution

### Interpreting Your Data

**High Interview Rate (>20%):**
- Your applications are well-targeted
- Your resume is effective
- Consider focusing on similar job types

**Low Interview Rate (<10%):**
- Consider revising your resume
- Apply to positions that better match your skills
- Personalize each application

**Long Average Response Time (>14 days):**
- Be patient; many companies have lengthy processes
- Consider following up with recruiters
- Continue applying to other positions

---

## Tips and Best Practices

### Job Application Management

**Be Consistent:**
- Enter job details immediately after applying
- Keep recruiter contact information up-to-date
- Use consistent formatting for company names

**Use the Notes Field:**
- Record where you found the posting
- Note any personal connections or referrals
- Track follow-up actions needed

**Track Resume Versions:**
- If you customize resumes, note which version you sent
- Use the Special Notes field to record customizations

### Interview Tracking

**Prepare Thoroughly:**
- Use Interview Notes to prepare questions
- Record interviewer names and titles for thank-you emails
- Update Interview Result promptly after each interview

**Follow Up:**
- Set reminders based on average response time
- Send thank-you emails within 24 hours
- Note follow-up dates in Interview Notes

### External Job Searching

**Be Specific:**
- Use detailed keywords (e.g., "React developer" vs. "developer")
- Specify location if you're not open to relocation
- Use Employment Type filters to avoid irrelevant results

**Search Regularly:**
- New jobs are posted daily
- The Daily Job Search Log prevents re-reviewing the same postings
- Consider searching at consistent times (e.g., every morning)

**Don't Rely on One Source:**
- Use both JSearch and USAJOBS
- Check company career pages directly
- Leverage your professional network

### USAJOBS-Specific Tips

**Understand Federal Hiring:**
- Federal applications often require detailed information
- Announcement closing dates are firm deadlines
- Some positions are only open to current federal employees or veterans

**Use Keywords from the Announcement:**
- Federal HR systems often use keyword matching
- Mirror the language in the job announcement
- Address all required qualifications

**Be Patient:**
- Federal hiring processes are longer than private sector
- It may take 30-90 days to hear back
- Don't let this discourage you from applying

### Analytics Usage

**Review Weekly:**
- Check your metrics every week
- Identify trends and adjust your strategy
- Celebrate progress (even small wins)

**Set Goals:**
- Aim for a specific number of applications per week
- Work to improve your interview rate over time
- Track month-over-month improvements

**Be Realistic:**
- Job searching takes time
- Industry and location affect response rates
- Quality applications matter more than quantity

### Data Hygiene

**Regular Cleanup:**
- Mark old, inactive applications as deleted
- Update interview results promptly
- Archive positions you're no longer interested in

**Backup Your Data:**
- Coordinate with your administrator for database backups
- Export important contact information periodically
- Keep copies of cover letters and resumes outside the system

---

## Troubleshooting

### Common Issues

**Search Returns No Results:**
- Broaden your keywords
- Remove location filters
- Check your date posted range

**Unable to Save Job Application:**
- Ensure required fields (Job Title, Recruiter Name) are filled
- Check for special characters that might cause errors
- Contact your administrator if the issue persists

**Interview Not Appearing:**
- Refresh the page
- Ensure the interview was saved successfully
- Check that you're viewing the correct job application

### Getting Help

If you encounter issues:
1. Check this user guide
2. Contact your system administrator
3. Report bugs via your organization's support channel

---

## Appendix: Field Reference

### Job Application Fields

| Field | Required | Description |
|-------|----------|-------------|
| Job Title | Yes | Position title |
| Job No | Auto | System-generated: JOB-YYYYMMDD-XXXX |
| Company Name | No | Hiring organization |
| Recruiting Agency | No | Third-party recruiter, if any |
| Recruiter Name | Yes | Primary contact |
| Recruiter Phone | No | Contact phone number |
| Recruiter Email | No | Contact email address |
| Job Location | No | City, state, or "Remote" |
| Salary | No | Offered compensation |
| Duration | No | Contract length (if applicable) |
| Employment Type | No | Full-time, Part-time, Contract, etc. |
| Work Mode | No | Remote, Hybrid, On-Site |
| Hybrid Days Count | No | Days per week in office (if Hybrid) |
| Requirements | No | Skills and qualifications |
| Job Description | No | Detailed role description |
| Special Notes | No | Any additional information |
| SubContract | No | Checkbox: Is this a subcontract role? |
| Resume Sent | No | Checkbox: Have you sent your resume? |
| Date Applied | Auto | Date the record was created |
| Resume Send Date | No | When you sent your resume |

### Interview Fields

| Field | Required | Description |
|-------|----------|-------------|
| Interview Date | Yes | Scheduled date and time |
| Interview Type | Yes | Phone, Video, or In-Person |
| Interviewer Name | No | Person conducting the interview |
| Interviewer Phone | No | Contact phone |
| Interviewer Email | No | Contact email |
| Interview Notes | No | Preparation notes, questions |
| Interview Result | No | Outcome or status |

---

**End of User Guide**

*For technical documentation, see the README.md file.*
*Copyright © Mesquite Information Technologies*
