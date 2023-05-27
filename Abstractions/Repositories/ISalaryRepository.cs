using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Abstractions.Repositories
{
    public interface ISalaryRepository : IRepositoryBase<Salary>
    {
        /// <summary>
        /// Returns the first Salary with the given <c>id</c>
        /// </summary>
        Task<Salary> GetSalaryById(int id);

        /// <summary>
        /// Returns the list of salaries generated for the given date
        /// </summary>
        Task<List<Salary>> GetSalaries(DateTime date);
    }
}
