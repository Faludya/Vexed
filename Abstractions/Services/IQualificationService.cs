using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vexed.Models;

namespace Abstractions.Services
{
    public interface IQualificationService
    {
        /// <summary>
        /// Creates a new Qualification
        /// </summary>
        Task CreateQualification(Qualification qualification);

        /// <summary>
        /// Updates the details of a Qualification.
        /// </summary>
        Task UpdateQualification(Qualification qualification);

        /// <summary>
        /// Removes the Qualification from the database.
        /// </summary>
        Task DeleteQualification(Qualification qualification);

        /// <summary>
        /// Returns the first Qualification with the given <c>id</c>
        /// </summary>
        Task<Qualification> GetQualificationById(int id);

        /// <summary>
        /// Returns all the Qualifications from the database.
        /// </summary>
        Task<List<Qualification>> GetAllQualifications();

        /// <summary>
        /// Returns all the Qualifications for a given user. 
        /// </summary>
        Task<List<Qualification>> GetQualifications(Guid userId);
    }
}
