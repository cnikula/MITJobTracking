# MITJobTracker — V10.0.0

**Copyright © Mesquite Information Technologies**
A Blazor Server web application for managing job applications, tracking interviews, and discovering
new job opportunities through an integrated external job search API.

---

## Overview

MITJobTracker is a personal job-search management tool that helps users stay organized throughout
the entire job application lifecycle — from finding opportunities to tracking application outcomes
and interview results. The application is deployed as an IIS sub-application under the path
`/mitJobTracker` and is designed for single-user or small-team use backed by a SQL Server database.

---

## Features

### Job Management
- Add, edit, and soft-delete job application records.
- Each record captures: job title, auto-generated job number (format: `JOB-YYYYMMDD-XXXX`),
  company name, recruiting agency, recruiter contact (name, phone, email), job location,
  salary, duration, employment type, work mode (Remote / Hybrid / On-Site), hybrid day count,
  requirements, job description, and special notes.
- SubContract and ResumeSend boolean flags for tracking application state.
- Date Applied and Resume Send Date are tracked per record.

### Interview Management
- Add, edit, and delete interview records linked to a job application.
- Fields include: interview date, interview type (phone, video, in-person), interviewer name,
  phone, email, interviewer notes, and interview result.
- Each interview record is associated with a Job via a foreign key (`JobId`).

### Detail View with Smart Change Detection
- The DetailView page merges Job and Interview data into a single `JobsInterviewDTO`.
- On page load, the DTO is deep-cloned using `Newtonsoft.Json` serialization.
- On save, the original and modified DTOs are JSON-serialized and compared field-by-field.
- Only the table(s) containing changed fields (Jobs, Interviews, or both) are written to the
  database, minimizing unnecessary round-trips.

### Job Prospect Grid
- The ViewProspect page displays all job applications in a paginated, sortable, searchable
  `SfGrid` (Syncfusion).
- Supports full-list and search-filtered views via the `usp_ViweProspect` stored procedure.
- Each row links through to the DetailView page for that record.

### Analytics Dashboard
- Displays four key metrics: total applications, active applications, interview rate, and
  average response time.
- Metrics are calculated by SQL Server stored procedures (`usp_GetInterviewRate`,
  `usp_GetAvgResponseTime`) and rendered as charts using `SfChart` (Syncfusion).

### External Job Search (JSearch API)
- The Job Search page integrates with the **JSearch API** (via RapidAPI) to search live job
  postings without leaving the application.
- Search parameters include: free-text query, location, date posted, country, language,
  employment type (multi-select), job requirements (multi-select), work-from-home toggle,
  page/pagination controls, radius, and publisher exclusions.
- Results are displayed in a sortable, paginated `SfGrid` with a direct Apply link per listing.
- API credentials are stored in `appsettings.json` under the `RapidApi` section.

### Version Display
- The application version (`10.0.0`) is read at startup from the assembly via `AppInfoService`
  and displayed on the home page.

### Error Handling
- All pages surface database and application errors through a Syncfusion `SfDialog` modal,
  keeping the user informed without crashing the UI.

---

## Technology Stack

| Category | Technology | Version |
|---|---|---|
| Runtime | .NET | 10.0 |
| Framework | ASP.NET Core Blazor Server | 10.0 |
| UI Components | Syncfusion Blazor | 26.1.41 |
| UI Theme | Syncfusion Bootstrap 5 Theme | 26.1.41 |
| CSS Framework | Bootstrap | 5.x |
| ORM | Entity Framework Core | 10.0.5 |
| Database Provider | EF Core SQL Server | 10.0.5 |
| Database | Microsoft SQL Server | — |
| JSON (change detection) | Newtonsoft.Json | 13.0.4 |
| JSON (API deserialization) | System.Text.Json | (built-in) |
| External Job API | JSearch via RapidAPI | REST/HTTPS |
| HTTP Client | IHttpClientFactory (named: "JSearch") | (built-in) |
| Scaffolding | Microsoft.VisualStudio.Web.CodeGeneration.Design | 10.0.2 |
| Licensing | Syncfusion.Licensing | 26.1.41 |

---

## Architecture

The application follows a layered **Factory → Service → Data** pattern with clear
separation of concerns:

```
Pages (Blazor Razor)
	└── IJobsFactory / JobsFactory          ← orchestrates business logic
			└── IJobsServices / JobsServices  ← data-access logic
					├── EFTableManagement     ← EF Core CRUD (Jobs, Interviews)
					└── CommonSP              ← ADO.NET stored procedure calls

Pages (Job Search)
	└── IJobSearchService / JobSearchService ← HTTP calls to JSearch RapidAPI
```

### Key Classes

| Class | Responsibility |
|---|---|
| `JobsFactory` | Validates inputs and delegates to `IJobsServices`; scoped |
| `JobsServices` | Implements all data operations; scoped |
| `EFTableManagement` | Wraps EF Core CRUD for `Job` and `Interview` entities |
| `CommonSP` | Executes SQL Server stored procedures via ADO.NET (`SqlCommand`) |
| `AppDBContext` | EF Core `DbContext`; defines `Job` and `Interview` DbSets |
| `AppInfoService` | Reads assembly version at startup; singleton |
| `JobSearchService` | Calls JSearch RapidAPI, builds query string, deserializes response; scoped |

### Dependency Injection (Program.cs)

| Service | Lifetime |
|---|---|
| `AppDBContext` | Scoped (EF Core default) |
| `IJobsFactory` / `JobsFactory` | Scoped |
| `IJobsServices` / `JobsServices` | Scoped |
| `IAppInfoService` / `AppInfoService` | Singleton |
| `IJobSearchService` / `JobSearchService` | Scoped |
| `HttpClient` (named: "JSearch") | Managed by `IHttpClientFactory` |

---

## Data Model

### Jobs Table (`Job` entity)

| Field | Type | Notes |
|---|---|---|
| `JobId` | int (PK) | Auto-increment primary key |
| `JobTitle` | string (75) | Required |
| `JobNo` | string (25) | Auto-generated: `JOB-YYYYMMDD-XXXX` |
| `CompanyName` | string (150) | Optional |
| `RecruitingAgency` | string (150) | Optional |
| `RecruitertName` | string (150) | Required |
| `RecruiterPhone` | string | Optional |
| `RecruiterEmail` | string | Optional |
| `JobLocation` | string | Optional |
| `Salary` | string | Default: "0" |
| `Duration` | string | Default: "N/A" |
| `EmploymentType` | string | Full Time, Contract, etc. |
| `Remote` / `Hybrid` / `OnSite` | bool | Work mode flags |
| `HybridNoOfDays` | int | Days on-site for hybrid roles |
| `SubContract` | bool | Default: false |
| `ResumeSend` | bool | Default: false |
| `DateApplied` | DateTime | Default: DateTime.Now |
| `ResumeSendDate` | DateTime | Optional |
| `Requirements` | string | Job requirements text |
| `JobDescription` | string | Full job description text |
| `Note` | string | Personal notes |
| `IsDeleted` | bool | Soft-delete flag (from `CommonModel`) |

### Interviews Table (`Interview` entity)

| Field | Type | Notes |
|---|---|---|
| `InterviewId` | int (PK) | Auto-increment primary key |
| `JobId` | int (FK) | Foreign key to Jobs |
| `InterviewDate` | DateTime | Required |
| `InterviewType` | string (150) | Required |
| `InterviewerName` | string (150) | Required |
| `InterviewerPhone` | string (15) | Optional |
| `InterviewerEmail` | string (150) | Optional |
| `InterviewerNotes` | string (500) | Optional |
| `InterviewerResulte` | string | Interview outcome notes |

---

## Database — Stored Procedures & Migrations

Stored procedures are created and versioned through EF Core migrations.

| Stored Procedure | Purpose |
|---|---|
| `usp_ViweProspect` | Returns paginated/filtered job prospect list for the grid |
| `sp_GetDetailInfo` | Returns merged Job + Interview detail for the DetailView page |
| `sp_SoftDeleteJobs` | Marks a job record as deleted without removing the row |
| `usp_GetInterviewRate` | Calculates the ratio of applications that reached an interview |
| `usp_GetAvgResponseTime` | Calculates the average days between application and first interview |

Database indexes are also managed via migrations for query performance.

---

## Project Structure

```
MITJobTracker/
├── Data/
│   ├── Job.cs                         Entity: Jobs table
│   ├── Interview.cs                   Entity: Interviews table
│   ├── AppDBContext.cs                EF Core DbContext
│   ├── Common/
│   │   ├── CommonModel.cs             Base class (IsDeleted, audit fields)
│   │   ├── CommonSP.cs                ADO.NET stored procedure executor
│   │   ├── EFTableManagement.cs       EF Core CRUD operations
│   │   └── EmploymentType.cs          Employment type lookup model
│   ├── DTOS/
│   │   ├── JobsInterviewDTO.cs        Merged Job + Interview view model
│   │   └── ProspectListDTO.cs         Prospect grid row model
│   └── Models/
│       └── JobSearch/
│           ├── JobSearchRequest.cs    JSearch API query parameters
│           └── JobSearchResponse.cs   JSearch API response model (root, JobListing, ApplyOption)
├── Factory/
│   ├── Interfaces/IJobsFactory.cs
│   └── JobsFactory.cs
├── Services/
│   ├── Interfaces/
│   │   ├── IJobsServices.cs
│   │   ├── IJobSearchService.cs
│   │   └── IAppInfoService.cs
│   ├── JobsServices.cs
│   ├── JobSearchService.cs
│   └── AppInfoService.cs
├── Pages/
│   ├── Index.razor                    Home / Add Job page
│   ├── ViewJobProspect.razor          Job prospect grid
│   ├── DetailView.razor               Job + Interview detail/edit
│   ├── Analytics.razor                Analytics dashboard
│   └── JobSearch.razor                External job search
├── Shared/
│   └── NavMenu.razor                  Navigation sidebar
├── Migrations/                        EF Core migration history
├── Program.cs                         DI registration, middleware pipeline
├── appsettings.json                   Connection strings, API keys, Syncfusion license
└── Pages/_Host.cshtml                 Blazor Server host page
```

---

## Configuration (appsettings.json)

```json
{
  "ConnectionStrings": {
	"mitLocalConnection": "Server=YOUR_SERVER;Database=YOUR_DB;..."
  },
  "SyncfusionLicenseKey": "YOUR_SYNCFUSION_LICENSE_KEY",
  "RapidApi": {
	"JSearchKey": "YOUR_RAPID_API_KEY",
	"JSearchHost": "jsearch.p.rapidapi.com",
	"JSearchBaseUrl": "https://jsearch.p.rapidapi.com/"
  }
}
```

> **Security Note:** Never commit real API keys or connection strings to source control.
> Use **User Secrets** (`dotnet user-secrets`) during development and environment variables
> or **Azure Key Vault** in production.

---

## Deployment

The application is configured for deployment as an IIS sub-application:

- Path base is set to `/mitJobTracker` via `app.UsePathBase("/mitJobTracker")` in `Program.cs`.
- The Syncfusion script (`syncfusion-blazor.min.js`) is referenced in `_Host.cshtml` inside
  `<head>` to ensure it loads before Blazor circuit hydration.
- HSTS and HTTPS redirection are enabled for non-development environments.

---

## Getting Started

1. **Clone the repository**
   ```
   git clone https://github.com/cnikula/MITJobTracking
   ```

2. **Configure the database connection**
   Update the `mitLocalConnection` value in `appsettings.json` to point to your SQL Server instance.

3. **Apply EF Core migrations**
   ```
   dotnet ef database update
   ```

4. **Add your Syncfusion license key**
   Set `SyncfusionLicenseKey` in `appsettings.json` or User Secrets.

5. **Add your RapidAPI key** (for Job Search feature)
   Set `RapidApi:JSearchKey` in `appsettings.json` or User Secrets.
   Sign up at https://rapidapi.com/letscrape-6bRBa3QguO5/api/jsearch

6. **Run the application**
   ```
   dotnet run
   ```
   Or press **F5** in Visual Studio.

---

## Version History

### V10.0.0 — .NET 10 Upgrade + Job Search Feature (Current)

**Changes from V9.4.1:**

- Target Framework upgraded from `net9.0` to `net10.0`.
- Application version bumped to `10.0.0` in `.csproj` (Version, AssemblyVersion, FileVersion).
- All Entity Framework Core packages upgraded from `9.x` to `10.0.5`.
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` updated to `10.0.2`.
- `Newtonsoft.Json` updated from `13.0.3` to `13.0.4`.
- `Program.cs`: Removed `IgnoreScriptIsolation = true` from `AddSyncfusionBlazor()` — the
  property was removed from Syncfusion's `GlobalOptions` API.
- `_Host.cshtml`: Syncfusion script reference moved from `<body>` to `<head>` to prevent
  hydration timing issues.
- **New Feature — External Job Search:** Added `JobSearch.razor` page with full parameter form
  (Syncfusion components), `JobSearchService`, `IJobSearchService`, `JobSearchRequest`,
  `JobSearchResponse` models, and RapidAPI JSearch integration via `IHttpClientFactory`.
- Syncfusion Blazor component packages remain at `26.x` (no component library major upgrade).

### V9.4.1 — Previous Release

- Blazor Server application targeting .NET 9.
- Core job and interview management features.
- Analytics dashboard with stored procedure integration.
- Deployed as IIS sub-application.
