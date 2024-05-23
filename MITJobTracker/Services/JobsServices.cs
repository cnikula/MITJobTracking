// ***********************************************************************
// Assembly         : MITJobTracker
// Author           : techn
// Created          : 04-22-2024
//
// Last Modified By : techn
// Last Modified On : 04-22-2024
// ***********************************************************************
// <copyright file="JobsServices.cs" company="MITJobTracker">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary>Jobs Services Class CRUD operations</summary>
// ***********************************************************************

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MITJobTracker.Data;
using MITJobTracker.Data.DTOS;
using MITJobTracker.Services.Interfaces;
using MITJobTracker.Data.Common;

namespace MITJobTracker.Services
{
    public class JobsServices : IJobsServices
    {
        protected readonly AppDBContext _context;
        private CommonSP _commonSP;

        public JobsServices(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _commonSP = new CommonSP(configuration);
        }

       
        
        public Task<Job> GetJobById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProspectListDTO>> GetJobList(string searchValue, bool fullList)
        {
           return await _commonSP.GetJobList(searchValue, fullList);
        }

       
        /// <summary>Adds the job.</summary>
        /// <param name="job">The job.</param>
        /// <returns>System.Int32.</returns>
        /// <remarks>Add new record to the Database table Jobs</remarks>
        public async Task<int> AddJob(Job job)
        {
            int returnValue = 0;

            try
            {
                //_context.Entry(job).State = EntityState.Added;

                _context.Jobs.Add(job);
                
                returnValue = await _context.SaveChangesAsync();

                if (returnValue > 0)
                {
                    return returnValue;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("");
                Console.WriteLine($"Error Message: {e.Message}, Error No. {e.HResult.ToString()}");
                return 0;
            }
        }

        public Task<Job> UpdateJobById(Job job)
        {
            throw new NotImplementedException();
        }

        public Task<Job> DeleteJob(int id)
        {
            throw new NotImplementedException();
        }
    }


}

