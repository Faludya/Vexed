using DataModels;

namespace Vexed.Services.Abstractions
{
    public interface IUserEmploymentService
    {
        /// <summary>
        /// Creates a new User Employment.
        /// </summary>
        Task CreateUserEmployment(UserEmployment userEmployment);

        /// <summary>
        /// Updates the details of a User Employment.
        /// </summary>
        Task UpdateUserEmployment(UserEmployment userEmployment);

        /// <summary>
        /// Removes the User Employment from the database.
        /// </summary>
        Task DeleteUserEmployment(UserEmployment userEmployment);

        /// <summary>
        /// Returns the first User Employment with the given <c>id</c>
        /// </summary>
        Task<UserEmployment> GetUserEmploymentById(int id);

        /// <summary>
        /// Returns the first User Employment with the given <c>userId</c>
        /// </summary>
        Task<UserEmployment> GetUserEmploymentByUserId(Guid userId);

        /// <summary>
        /// Returns all the User Employment from the database.
        /// </summary>
        Task<List<UserEmployment>> GetAllUsersEmployment();

        /// <summary>
        /// Returns all the User Employment for a given user.
        /// </summary>
        Task<List<UserEmployment>> GetUsersEmployment(Guid userId);
    }
}
