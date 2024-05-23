// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : techn : Claude Nikula
// Created          : 03-13-2024
//
// Last Modified By : techn : Claude Nikula
// Last Modified On : 03-13-2024
// ***********************************************************************
// <copyright file="AppDBContext.cs" company="Mesquite IT">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>
// This C# code defines a class AppDBContext that inherits from the DbContext
// class provided by the Entity Framework Core (EF Core) library. EF Core is
// a popular Object-Relational Mapping (ORM) framework for .NET. It allows
// developers to work with databases using .NET objects, and eliminates the
// need for most of the data-access code that developers usually need to
// write.
// </summary>
// ***********************************************************************

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MITJobTracker.Data.DTOS;


namespace MITJobTracker.Data
{
    public class AppDBContext : DbContext
    {
       // internal readonly object ProspectListDTOs;

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Interview> Interviews { get; set; }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<ProspectListDTO>().HasNoKey();
        }


    }

}
