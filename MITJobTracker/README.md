Overall Summary of MITJobTracker Application — V9.4.1

MITJobTracker is a Blazor Server web application designed to help users manage job applications and interviews.
The application targets .NET 9, uses Entity Framework Core 9 for data access, Syncfusion Blazor 26.x for UI
components, and SQL Server stored procedures for reporting queries.

Primary Functionalities

1.	Job Management: Users can add, update, and soft-delete job records. Each job record contains detailed
	information such as job title, job number, company name, job location, salary, duration, employment type,
	work-mode (Remote / Hybrid / On Site), recruiter details, requirements, description, and special notes.
2.	Interview Management: Users can add, update, and delete interview records associated with job applications.
	Each interview record includes details such as interview date, interview type, interviewer name, phone,
	email, interviewer notes, and interview results.
3.	Detail View & Change Detection: The DetailView page loads a merged JobsInterviewDTO, clones it on entry,
	and uses JSON-based comparison (Newtonsoft.Json) to detect which fields changed. Only the affected table(s)
	— Jobs, Interviews, or both — are updated, reducing unnecessary database writes.
4.	Job Prospect Listing: The ViewProspect page provides a searchable, sortable, and paginated grid
	(Syncfusion SfGrid) of all job prospects, with click-through navigation to the Detail View.
5.	Analytics Dashboard: The Analytics page displays key metrics — total applications, active applications,
	interview rate, and average response time — powered by SQL Server stored procedures and rendered with
	Syncfusion SfChart.
6.	Data Persistence: The application uses Entity Framework Core to interact with a SQL Server database.
	The AppDBContext class defines the database context, and the EFTableManagement class provides methods
	for CRUD operations on job and interview records via EF. The CommonSP class executes stored procedures
	for list retrieval and analytics queries.
7.	Version Display: The application version (set in the .csproj as 9.4.1) is read at runtime by the
	AppInfoService and displayed on the home page.
8.	User Interface: The application uses Blazor Server-Side rendering with Syncfusion Blazor components
	(SfGrid, SfDropDownList, SfCheckBox, SfButton, SfDialog, SfChart, SfTextBox, SfIcon) and Bootstrap 5
	for styling and layout.
9.	Error Handling: The application includes error handling with modal dialog notifications (SfDialog)
	to surface database and application errors to the user.

Architecture

-	Pattern: Factory → Service → Data (EF / Stored Procedures)
-	IJobsFactory / JobsFactory: Orchestrates business operations and delegates to IJobsServices.
-	IJobsServices / JobsServices: Contains data-access logic using EFTableManagement and CommonSP.
-	EFTableManagement: Entity Framework CRUD operations for Jobs and Interviews.
-	CommonSP: ADO.NET calls to SQL Server stored procedures for list and analytics data.
-	DTOs: JobsInterviewDTO (merged Job + Interview view), ProspectListDTO (grid listing).

Brief Summary on the Migration File Pertaining to Stored Procedures for SQL Server

In the context of Entity Framework Core, migrations are used to manage changes to the database schema over time.
A migration file for stored procedures typically includes the creation, modification, or deletion of stored
procedures in the SQL Server database. Key migrations include stored procedures for the prospect view
(usp_ViweProspect), detail info retrieval (sp_GetDetailInfo), soft-delete (sp_SoftDeleteJobs), interview rate
calculation (usp_GetInterviewRate), average response time (usp_GetAvgResponseTime), and database indexes
for performance.

NOTE: Be sure to update the connection string in the appsettings.json file to point to your own SQL Server database.
