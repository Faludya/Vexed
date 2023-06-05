using DataModels;
using Microsoft.AspNetCore.Http;

namespace Abstractions.Services
{
    public interface IQualificationService
    {
        /// <summary>
        /// Creates a new Qualification
        /// </summary>
        Task CreateQualification(Qualification qualification, IFormFile attachmentFile);

        /// <summary>
        /// Updates the details of a Qualification.
        /// </summary>
        Task UpdateQualification(Qualification qualification, IFormFile attachmentFile);

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
