﻿using Vexed.Repositories;
using Abstractions.Repositories;
using DataModels;
using Shared;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SalaryRepository : RepositoryBase<Salary>, ISalaryRepository
    {
        private readonly Logger _logger;
        public SalaryRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<List<Salary>> GetSalaries(DateTime date)
        {
            try
            {
                var startOfMonth = new DateTime(date.Year, date.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                var salaries = await _vexedDbContext.Salaries!
                    .Where(s => s.GeneratedDate >= startOfMonth && s.GeneratedDate <= endOfMonth)
                    .ToListAsync();

                return salaries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    

        public async Task<Salary> GetSalaryById(int id)
        {
            try
            {
                return await _vexedDbContext.Salaries!.Where(s => s.Id == id).FirstAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
