Overall Summary of MITJobTracker Application — V10.0.0

MITJobTracker is a Blazor Server web application designed to help users manage job applications and interviews.
The application targets .NET 10, uses Entity Framework Core 10.0.5 for data access, Syncfusion Blazor 26.x for UI
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
7.	Version Display: The application version (set in the .csproj as 10.0.0) is read at runtime by the
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

---

Version History

V10.0.0 — .NET 10 Upgrade (Current)

Changes from V9.4.1:

-	Target Framework: Upgraded from net9.0 to net10.0.
-	Application Version: Bumped from 9.4.1 to 10.0.0 in the .csproj (Version, AssemblyVersion, FileVersion).
-	Entity Framework Core: Upgraded all EF Core packages (Microsoft.EntityFrameworkCore,
	Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Design,
	Microsoft.EntityFrameworkCore.Tools) from 9.x to 10.0.5.
-	Microsoft.VisualStudio.Web.CodeGeneration.Design: Updated to 10.0.2.
-	Newtonsoft.Json: Updated from 13.0.3 to 13.0.4.
-	Program.cs: Removed the IgnoreScriptIsolation = true option from AddSyncfusionBlazor() — this
	option was removed from the Syncfusion GlobalOptions API and is no longer required.
-	_Host.cshtml: Moved the Syncfusion script reference (syncfusion-blazor.min.js) from the
	end of <body> to the <head> section to ensure scripts are available before component
	hydration begins.
-	Syncfusion Blazor: Remains at 26.x (no Syncfusion major version upgrade in this release).
