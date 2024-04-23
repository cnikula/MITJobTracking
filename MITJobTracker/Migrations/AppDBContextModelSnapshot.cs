﻿// <auto-generated />
using System;
using MITJobTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MITJobTracker.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MITJobTracker.Data.Interview", b =>
                {
                    b.Property<int>("InterviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("InterviewId, Table primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterviewId"));

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("External foreign.");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and Time record was create.");

                    b.Property<string>("DeleteByID")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("External foreign.");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and Time record was create.");

                    b.Property<DateTime>("InterviewDate")
                        .HasColumnType("datetime2")
                        .HasComment("Interview Date, the date of the interview");

                    b.Property<string>("InterviewType")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Interview Type, is interview by phone, in-person or via computer. Example Zoome, Teams, Sky ");

                    b.Property<string>("InterviewerEmail")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Interviewer Email, the person email that is interviewing you");

                    b.Property<string>("InterviewerName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Interviewer Name, the person full name that is interviewing you");

                    b.Property<string>("InterviewerNotes")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Interviewer Notes, any notes that you want to keep about the interview");

                    b.Property<string>("InterviewerPhone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasComment("Interviewer Phone, the person phone number that is interviewing you");

                    b.Property<string>("InterviewerResulte")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasComment("Interviewer Result, the result of the interview");

                    b.Property<int>("JobId")
                        .HasColumnType("int")
                        .HasComment("JobId, foreign key to the Jobs table");

                    b.Property<string>("ModifiedById")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("External foreign.");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and Time record was create.");

                    b.HasKey("InterviewId");

                    b.HasIndex("JobId");

                    b.ToTable("Interviews");
                });

            modelBuilder.Entity("MITJobTracker.Data.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Table primary key");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<string>("CompanyName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Company Name, the company name you are applying for.");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("External foreign.");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and Time record was create.");

                    b.Property<DateTime>("DateApplied")
                        .HasColumnType("datetime2")
                        .HasComment("DateApplied, the date you Applied for the job or send bid.");

                    b.Property<string>("DeleteByID")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("External foreign.");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and Time record was create.");

                    b.Property<string>("Duration")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasComment("Duration, how long the job is for.");

                    b.Property<string>("EmploymentType")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasComment("Employment Type, W2, 1099, Part-Time, Full-Time.");

                    b.Property<bool>("Hybrid")
                        .HasColumnType("bit")
                        .HasComment("Hybrid, if the job is hybrid.");

                    b.Property<string>("HybridNoOfDays")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasComment("Hybrid No Of Days, how may days on-site");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Job Description, the job description.");

                    b.Property<string>("JobLocation")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Job Location, the job location.");

                    b.Property<string>("JobNo")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasComment("Job Number, position identifier for the job you are appalling for.");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)")
                        .HasComment("Job Title, description of the title");

                    b.Property<string>("ModifiedById")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("External foreign.");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasComment("Date and Time record was create.");

                    b.Property<bool>("OnSite")
                        .HasColumnType("bit")
                        .HasComment("OnSite, if the job is on site.");

                    b.Property<string>("RecruiterEmail")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Recruiter Email, the agency person you are communicating with email.");

                    b.Property<string>("RecruiterPhone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasComment("Recruiter Phone, the agency person you are communicating with phone number.");

                    b.Property<string>("RecruitertName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Recruiter Name, the agency person you are communicating with");

                    b.Property<string>("RecruitingAgency")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("Recruiting Agency, the agency that is helping you to get the job. Note this can be the same as CompanyName ");

                    b.Property<bool>("Remote")
                        .HasColumnType("bit")
                        .HasComment("Remote, if the job is remote.");

                    b.Property<string>("Requirements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Job Requirements, the job requirements.");

                    b.Property<bool>("ResumeSend")
                        .HasColumnType("bit")
                        .HasComment("Resume Send, did you send a resume out");

                    b.Property<DateTime?>("ResumeSendDate")
                        .HasColumnType("datetime2")
                        .HasComment("Resume Send Date, the date you send the resume out or applied for the job. If resume send then ResumeSend is set to 1 (true)");

                    b.Property<string>("Salary")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)")
                        .HasComment("Salary, pay range per hour our salary");

                    b.Property<bool>("SubContract")
                        .HasColumnType("bit")
                        .HasComment("Sub-Contract, if you contract through another company. Example Head-hunter");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("MITJobTracker.Data.Interview", b =>
                {
                    b.HasOne("MITJobTracker.Data.Job", null)
                        .WithMany("Interviews")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MITJobTracker.Data.Job", b =>
                {
                    b.Navigation("Interviews");
                });
#pragma warning restore 612, 618
        }
    }
}
