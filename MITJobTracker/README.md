# MITJobTracker — V10.27.0

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
- **Search state is preserved** across page navigation using a Scoped state service
  (`IJobSearchStateService`). When you navigate away and return, your search criteria and
  results are restored instantly without consuming an additional API call.
- API credentials are stored in `appsettings.json` under the `RapidApi` section.

### Version Display
- The application version (`10.27.0`) is read at startup from the assembly via `AppInfoService`
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
| `JobSearchStateService` | Scoped state container — preserves Job Search page criteria and results across Blazor navigation within the same circuit |

### Dependency Injection (Program.cs)

| Service | Lifetime |
|---|---|
| `AppDBContext` | Scoped (EF Core default) |
| `IJobsFactory` / `JobsFactory` | Scoped |
| `IJobsServices` / `JobsServices` | Scoped |
| `IAppInfoService` / `AppInfoService` | Singleton |
| `IJobSearchService` / `JobSearchService` | Scoped |
| `IJobSearchStateService` / `JobSearchStateService` | Scoped |
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

### DailyJobSearchLogs Table (`DailyJobSearchLog` entity)

Tracks external job IDs retrieved from the JSearch API per calendar day. Used to suppress
duplicate results across multiple searches within the same day and to record which jobs
the user has already reviewed.

| Field | Type | Notes |
|---|---|---|
| `Id` | int (PK) | Auto-increment primary key |
| `ExternalJobId` | string (256) | Job ID returned by the JSearch API. Required |
| `SearchDate` | DateTime | Calendar date (UTC) on which the job was retrieved |
| `RetrievedAtUtc` | DateTime | UTC timestamp when the record was first created |
| `IsReviewed` | bool | True when the user has marked this job as reviewed. Default: false |
| `ReviewedAtUtc` | DateTime? | UTC timestamp when the job was marked reviewed. Null if not reviewed |

> A composite index on `(ExternalJobId, SearchDate)` enforces fast duplicate-suppression lookups.

---

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
│   │   ├── IJobSearchStateService.cs
│   │   └── IAppInfoService.cs
│   ├── JobsServices.cs
│   ├── JobSearchService.cs
│   ├── JobSearchStateService.cs
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

> **⚠️ IMPORTANT — You must configure the following before running the application.**
> The committed `appsettings.json` intentionally does **not** contain secrets.
> All three items below are commented out in the file and must be provided by you.

### 1. SQL Server Connection String (Required)

You **must** set your own connection string. Uncomment and update the `ConnectionStrings`
section in `appsettings.json` to point to your SQL Server instance:

```json
"ConnectionStrings": {
  "mitLocalConnection": "Data Source=YOUR_SERVER;Initial Catalog=MITJobTracker;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
}
```

Or use **User Secrets** (recommended for development):

```powershell
cd MITJobTracker
dotnet user-secrets set "ConnectionStrings:mitLocalConnection" "Data Source=YOUR_SERVER;Initial Catalog=MITJobTracker;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
```

### 2. Syncfusion License Key (Required)

> **The Syncfusion license key is not included in the repository.**
> It will be **provided by the developer (Claude Nikula) on request**.
> Please contact the developer directly to obtain the key.

Once you have the key, uncomment and set the `SyncfusionLicenseKey` value in
`appsettings.json`:

```json
"SyncfusionLicenseKey": "YOUR_KEY_HERE"
```

Or use **User Secrets**:

```powershell
dotnet user-secrets set "SyncfusionLicenseKey" "YOUR_KEY_HERE"
```

Without this key the application will run but Syncfusion components will display a
trial/license watermark banner.

### 3. RapidAPI Key — JSearch (Optional — for Job Search feature)

The external Job Search page requires a RapidAPI key for the JSearch API.
Sign up at https://rapidapi.com/letscrape-6bRBa3QguO5/api/jsearch and then set:

```json
"RapidApi": {
  "JSearchKey": "YOUR_RAPID_API_KEY",
  "JSearchHost": "jsearch.p.rapidapi.com",
  "JSearchBaseUrl": "https://jsearch.p.rapidapi.com/"
}
```

Or use **User Secrets**:

```powershell
dotnet user-secrets set "RapidApi:JSearchKey" "YOUR_RAPID_API_KEY"
```

The rest of the application (job management, interviews, analytics) works without this key.

> **Security Note:** Never commit real API keys, license keys, or connection strings to
> source control. Use **User Secrets** (`dotnet user-secrets`) during development and
> environment variables or **Azure Key Vault** in production.

---

## Deployment

The application is configured for deployment as an IIS sub-application:

- Path base is set to `/mitJobTracker` via `app.UsePathBase("/mitJobTracker")` in `Program.cs`.
- The Syncfusion script (`syncfusion-blazor.min.js`) is referenced in `_Host.cshtml` inside
  `<head>` to ensure it loads before Blazor circuit hydration.
- HSTS and HTTPS redirection are enabled for non-development environments.

---

## SQL Server Express — Editions & Licensing

MITJobTracker requires Microsoft SQL Server. **SQL Server Express** is a free edition that
can be redistributed with your application at no cost.

### SQL Server Express Editions

| Edition | Description |
|---|---|
| **SQL Server Express (Core Engine)** | The basic database engine. Free to distribute with your applications. |
| **SQL Server Express with Tools** | Includes the SQL Server Management Studio (SSMS) installer alongside the engine. |
| **SQL Server Express LocalDB** | A lightweight, file-based SQL Server instance that runs in user mode. Perfect for desktop apps and development. |
| **SQL Server Express with Advanced Services** | Includes Full-Text Search and Reporting Services (limited) in addition to the core engine. |

> **Recommendation:** For local development, **SQL Server Express LocalDB** is the simplest
> option — it installs with Visual Studio and requires no configuration. For deployment or
> shared environments, use **SQL Server Express** or **Express with Advanced Services**.

### Licensing Summary

✅ SQL Server Express is **free to distribute** with your application.

- No runtime royalties.
- No per-user or per-server fees.
- Your end users do **not** need to purchase SQL Server licenses.
- The free tier is limited to 1 GB RAM, 10 GB max database size, and 1 physical CPU — more
  than sufficient for the single-user / small-team use case of MITJobTracker.
- If capacity requirements grow beyond Express limits, upgrade to SQL Server Standard or
  Enterprise (separate license required).

### Download

- **SQL Server Express:** https://www.microsoft.com/en-us/sql-server/sql-server-downloads
- **SQL Server Management Studio (SSMS):** https://learn.microsoft.com/en-us/ssms/download-sql-server-management-studio-ssms

---

## Prerequisites

- **.NET 10 SDK** — https://dotnet.microsoft.com/download/dotnet/10.0
- **SQL Server** (LocalDB, Express, Developer, or full edition) — see **SQL Server Express** section above
- **Visual Studio 2026** (recommended) or the `dotnet` CLI
- **Syncfusion license key** — contact the developer (Claude Nikula) to obtain
- **RapidAPI account** (optional) — only needed for the external Job Search feature

---

## Getting Started

1. **Clone the repository**
   ```
   git clone https://github.com/cnikula/MITJobTracking
   cd MITJobTracking
   ```

2. **Configure your SQL Server connection string** ⚠️
   This is required — the application will not start without it.
   Uncomment and update the `ConnectionStrings` section in `appsettings.json`,
   or set it via User Secrets (see **Configuration** section above for details).

3. **Obtain and configure the Syncfusion license key** ⚠️
   Contact the developer (**Claude Nikula**) to request the Syncfusion license key.
   Set it in `appsettings.json` or User Secrets (see **Configuration** section above).

4. **Apply EF Core migrations**
   ```
   dotnet ef database update --project MITJobTracker
   ```
   This creates the database, tables, stored procedures, and indexes.

5. **Configure the RapidAPI key** (optional)
   Only needed if you want to use the Job Search page.
   Sign up at https://rapidapi.com/letscrape-6bRBa3QguO5/api/jsearch
   and set `RapidApi:JSearchKey` in `appsettings.json` or User Secrets.

6. **Run the application**
   ```
   dotnet run --project MITJobTracker
   ```
   Or open `MITJobTracker.sln` in Visual Studio and press **F5**.

---

## Version History

### V10.27.0 — Bug Fixes & Database Cleanup (Current)

**Changes from V10.26.0:**

**Bug Fix — Analytics Chart Rendering:**
- `Analytics.razor`: Added `OnAfterRenderAsync(firstRender)` alongside `OnInitializedAsync`
  to fix a Syncfusion `SfChart` rendering issue where the chart would not render when
  navigating directly to the Analytics page before visiting the ViewProspect page.
- `OnInitializedAsync` still loads all metric data (cards and chart data source).
- `OnAfterRenderAsync` calls `StateHasChanged()` on first render to signal the chart to
  mount with the already-loaded data once the DOM is fully ready.

**Database Cleanup — Drop ProspectListDTO Table:**
- `AppDBContext.cs`: Updated `OnModelCreating` to add `.ToView(null)` to the
  `ProspectListDTO` keyless entity configuration. This correctly marks it as a
  raw-SQL-result mapping type only, preventing EF Core from creating or managing
  it as a physical table.
- Added migration `DropProspectListDTOTable` to drop the `ProspectListDTO` table
  that was incorrectly created in the database during a prior migration. The `Down`
  method is intentionally empty — this table should never have existed.
- `ProspectListDTO` remains a valid DTO used by `CommonSP.cs` to map results from
  the `usp_ViweProspect` stored procedure via ADO.NET.

---

### V10.26.0 — .NET 10 Upgrade + Job Search + State Management

**Changes from V9.4.1 (previous release):**

**Framework & Packages:**
- Target Framework upgraded from `net9.0` to `net10.0`.
- Application version bumped to `10.0.0` in `.csproj` (Version, AssemblyVersion, FileVersion).
- All Entity Framework Core packages upgraded from `9.x` to `10.0.5`.
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` updated to `10.0.2`.
- `Newtonsoft.Json` updated from `13.0.3` to `13.0.4`.
- Syncfusion Blazor component packages remain at `26.x` (no component library major upgrade).

**Program.cs & Middleware:**
- Removed `IgnoreScriptIsolation = true` from `AddSyncfusionBlazor()` — the property was
  removed from Syncfusion's `GlobalOptions` API.
- Removed dead `ConfigureServices` local function that was declared but never called.
- Moved `UsePathBase("/mitJobTracker")` before `UseStaticFiles()` so IIS sub-app
  deployment resolves static files and routes correctly.
- Removed commented-out `AddTransient` registrations.

**_Host.cshtml:**
- Syncfusion script reference moved from `<body>` to `<head>` to prevent hydration
  timing issues.

**New Feature — External Job Search:**
- Added `JobSearch.razor` page with full parameter form (Syncfusion components) and
  results grid (`SfGrid`) with Apply links.
- Added `JobSearchService`, `IJobSearchService`, `JobSearchRequest`, `JobSearchResponse`
  models, and RapidAPI JSearch integration via `IHttpClientFactory`.
- Job Search form layout uses Bootstrap grid (`row`/`col`) instead of fragile
  margin-based label positioning for proper horizontal and vertical alignment.

**New Feature — Search State Preservation:**
- Added `IJobSearchStateService` / `JobSearchStateService` (Scoped) to preserve
  the Job Search page's criteria and results across Blazor navigation.
- Navigating away and returning restores the form and grid instantly without
  consuming an additional API call, reducing API cost.
- A "Last searched" timestamp badge shows when results were fetched.
- The Clear button resets both the form and the persisted state.

**Security — Secrets Removed from Source Control:**
- `appsettings.json` no longer contains real API keys, license keys, or connection
  strings. All sensitive values are commented out with placeholder instructions.
- Developers must configure their own connection string, Syncfusion key (provided by
  the developer on request), and RapidAPI key via `appsettings.json` or User Secrets.

**Bug Fixes — JSearch API Deserialization:**
- `JobSearchResponse.cs`: Changed `job_is_remote`, `job_apply_is_direct`, and
  `ApplyOption.is_direct` from `bool` to `bool?` — the API returns `null` for
  these fields on some listings.
- Changed `job_latitude` and `job_longitude` from `double` to `double?` for the
  same reason.

### V9.4.1 — Previous Release

- Blazor Server application targeting .NET 9.
- Core job and interview management features.
- Analytics dashboard with stored procedure integration.
- Deployed as IIS sub-application.
