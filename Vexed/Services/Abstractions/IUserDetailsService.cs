using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IUserDetailsService
    {
        /// <summary>
        /// Creates a new User Detail.
        /// </summary>
        void CreateUserDetails(UserDetails userDetails);

        /// <summary>
        /// Updates the details of a User Detail.
        /// </summary>
        void UpdateUserDetails(UserDetails userDetails);

        /// <summary>
        /// Removes the User Detail from the database.
        /// </summary>
        void DeleteUserDetails(UserDetails userDetails);

        /// <summary>
        /// Returns the first User Detail with the given <c>id</c>
        /// </summary>
        UserDetails GetUserDetailsById(int id);

        /// <summary>
        /// Returns all the User Details from the database.
        /// </summary>
        List<UserDetails> GetAllUsersDetails();

        /// <summary>
        /// Returns all the User Details for a given user.
        /// </summary>
        List<UserDetails> GetUsersDetails(Guid userId);
    }
}
