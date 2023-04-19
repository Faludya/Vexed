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
    public interface IQualificationRepository : IRepositoryBase<Qualification>
    {
        /// <summary>
        /// Returns the first Qualification with the given <c>id</c>
        /// </summary>
        Task<Qualification> GetQualificationById(int id);

        /// <summary>
        /// Returns all the Qualifications for a given user. 
        /// </summary>
        Task<List<Qualification>> GetQualifications(Guid userId);
    }
}
