using DataModels;

namespace Vexed.Repositories.Abstractions
{
    public interface IUserEmploymentRepository : IRepositoryBase<UserEmployment>
    {
        /// <summary>
        /// Returns the first User Employment with the given <c>id</c>
        /// </summary>
        Task<UserEmployment?> GetUserEmploymentById(int id);

        /// <summary>
        /// Returns the first User Employment with the given <c>userId</c>
        /// </summary>
        Task<UserEmployment?> GetUserEmploymentByUserId(Guid userId);

        /// <summary>
        /// Returns all the User Employment for a given user.
        /// </summary>
        Task<List<UserEmployment>> GetUserEmployments(Guid userId);

        /// <summary>
        /// Returns all the UserIds from the database
        /// </summary>
        Task<List<string>> GetAllUserEmploymentIds();

        /// <summary>
        /// Get the superior for the given user
        /// </summary>
        Task<UserEmployment?> GetUserSuperior(Guid userId);
        /// <summary>
        /// Get the employments for the subordinates of the superior
        /// </summary>
        Task<List<UserEmployment>> GetTeamMembersEmployment(Guid superiorId);
    }

}