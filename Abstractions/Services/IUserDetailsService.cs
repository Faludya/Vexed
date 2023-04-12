using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IUserDetailsService
    {
        /// <summary>
        /// Creates a new User Detail.
        /// </summary>
        Task CreateUserDetails(UserDetails userDetails);

        /// <summary>
        /// Updates the details of a User Detail.
        /// </summary>
        Task UpdateUserDetails(UserDetails userDetails);

        /// <summary>
        /// Removes the User Detail from the database.
        /// </summary>
        Task DeleteUserDetails(UserDetails userDetails);

        /// <summary>
        /// Returns the first User Detail with the given <c>id</c>
        /// </summary>
        Task<UserDetails> GetUserDetailsById(int id);

        /// <summary>
        /// Returns the first User Detail with the given <c>userId</c>
        /// </summary>
        Task<UserDetails> GetUserDetailsByUserId(Guid userId);

        /// <summary>
        /// Returns all the User Details from the database.
        /// </summary>
        Task<List<UserDetails>> GetAllUsersDetails();

        /// <summary>
        /// Returns all the User Details for a given user.
        /// </summary>
        Task<List<UserDetails>> GetUsersDetails(Guid userId);
    }
}
