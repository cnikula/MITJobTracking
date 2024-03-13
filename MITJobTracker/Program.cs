// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : techn : Claude Nikula
// Created          : 03-13-2024
//
// Last Modified By : techn : Claude Nikula
// Last Modified On : 03-13-2024
// ***********************************************************************
// <copyright file="Program.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary> This C# code is the main entry point of an ASP.NET Core
// application. It sets up the web server, configures the HTTP request
// pipeline, and starts the application. Here's a breakdown of what the
// code does:
// </summary>
// ***********************************************************************


using Microsoft.EntityFrameworkCore;
using MITJobTracker.Data;

var builder = WebApplication.CreateBuilder(args);

// Initialize the Configuration object
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

#region Connection String

// Retrieve the connection string using the Configuration object
builder.Services.AddDbContext<AppDBContext>(item => item.UseSqlServer(configuration.GetConnectionString("mitLocalConnection")));

// ...
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
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
