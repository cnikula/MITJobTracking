using Microsoft.EntityFrameworkCore;
using MITJobTracker.Data;
using MITJobTracker.Factory;
using MITJobTracker.Factory.Interfaces;
using MITJobTracker.Services;
using MITJobTracker.Services.Interfaces;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Initialize the Configuration object
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });
builder.Services.AddSyncfusionBlazor();

#region Connection String

// Retrieve the connection string using the Configuration object
builder.Services.AddDbContext<AppDBContext>(item => item.UseSqlServer(configuration.GetConnectionString("mitLocalConnection")));
//builder.Services.AddTransient<IJobsFactory, JobsFactory>();
builder.Services.AddScoped<IJobsFactory, JobsFactory>();
//builder.Services.AddTransient<IJobsServices, JobsServices>();
builder.Services.AddScoped<IJobsServices, JobsServices>();
builder.Services.AddSingleton<MITJobTracker.Services.Interfaces.IAppInfoService, MITJobTracker.Services.AppInfoService>();

// ...
#endregion

#region External API Clients

// JSearch API via RapidAPI
builder.Services.AddHttpClient("JSearch", client =>
{
    client.BaseAddress = new Uri(configuration["RapidApi:JSearchBaseUrl"] ?? "https://jsearch.p.rapidapi.com/");
});
builder.Services.AddScoped<IJobSearchService, JobSearchService>();

// Scoped state — persists across navigation within the same browser tab
builder.Services.AddScoped<IJobSearchStateService, JobSearchStateService>();

#endregion

var app = builder.Build();

// 26.X.X - Syncfusion.Licensing
var syncfusionLicenseKey = configuration["SyncfusionLicenseKey"];
if (!string.IsNullOrEmpty(syncfusionLicenseKey))
{
    Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenseKey);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UsePathBase("/mitJobTracker");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

