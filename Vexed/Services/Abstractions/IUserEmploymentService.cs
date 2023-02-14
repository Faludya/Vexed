using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IUserEmploymentService
    {
        /// <summary>
        /// Creates a new User Employment.
        /// </summary>
        void CreateUserEmployment(UserEmployment userEmployment);

        /// <summary>
        /// Updates the details of a User Employment.
        /// </summary>
        void UpdateUserEmployment(UserEmployment userEmployment);

        /// <summary>
        /// Removes the User Employment from the database.
        /// </summary>
        void DeleteUserEmployment(UserEmployment userEmployment);

        /// <summary>
        /// Returns the first User Employment with the given <c>id</c>
        /// </summary>
        UserEmployment GetUserEmploymentById(int id);

        /// <summary>
        /// Returns all the User Employment from the database.
        /// </summary>
        List<UserEmployment> GetAllUsersEmployment();

        /// <summary>
        /// Returns all the User Employment for a given user.
        /// </summary>
        List<UserEmployment> GetUsersEmployment(Guid userId);
    }
}
