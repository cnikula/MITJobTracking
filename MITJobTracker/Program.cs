using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

// Configure the HTTP request pipeline.

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzE4NTY5OUAzMjM1MmUzMDJlMzBlMUhoT1ByL0NOczVtcDV4OUprQ3pZT09DbmNPeXR2MU1vUlhzdjJXNC9RPQ==;Mgo+DSMBPh8sVXJxS0d+X1RPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9nSX1RcERgXX9fd3ZcRWc=;ORg4AjUWIQA/Gnt2UFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hTX5QdENiWntecXNXT2Nb;Mgo+DSMBMAY9C3t2UFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hTX5QdENiWntecXNWRmlY;MzE4NTcwM0AzMjM1MmUzMDJlMzBYSW81eHMzb1hQMXF4Z0pYS3NDMDI2ZnA0Skdqc1ZyRVJHcklieVNHMVdRPQ==");

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

