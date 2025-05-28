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
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSyncfusionBlazor();

#region Connection String

// Retrieve the connection string using the Configuration object
void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<AppDBContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("mitLocalConnection")));
}

builder.Services.AddDbContext<AppDBContext>(item => item.UseSqlServer(configuration.GetConnectionString("mitLocalConnection")));

//builder.Services.Add<IJobsFactory, JobsFactory>();
builder.Services.AddTransient<IJobsFactory, JobsFactory>();
//builder.Services.AddSingleton<IJobsServices, JobsServices>();
builder.Services.AddTransient<IJobsServices, JobsServices>();


// ...
#endregion




var app = builder.Build();


// 26.X.X - Syncfusion.Licensing
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("<License token gos here>");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

