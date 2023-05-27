using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Repositories;
using Abstractions.Repositories;
using DataModels;
using Shared;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SalaryRepository : RepositoryBase<Salary>, ISalaryRepository
    {
        private Logger _logger;
        public SalaryRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<List<Salary>> GetSalaries(DateTime date)
        {
            try
            {
                var startOfMonth = new DateTime(date.Year, date.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                var salaries = _vexedDbContext.Salaries
                    .Where(s => s.GeneratedDate >= startOfMonth && s.GeneratedDate <= endOfMonth)
                    .ToList();

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
                return await _vexedDbContext.Salaries.Where(s => s.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
