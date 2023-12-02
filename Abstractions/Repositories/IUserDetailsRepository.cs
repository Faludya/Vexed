using DataModels;

namespace Vexed.Repositories.Abstractions
{
    public interface IUserDetailsRepository : IRepositoryBase<UserDetails>
    {
        /// <summary>
        /// Returns the first User Detail with the given <c>id</c>
        /// </summary>
        Task<UserDetails?> GetUserDetailsById(int id);

        /// <summary>
        /// Returns the first User Detail with the given <c>userId</c>
        /// </summary>
        Task<UserDetails?> GetUserDetailsByUserId(Guid userId);

        /// <summary>
        /// Returns all the User Details for a given user.
        /// </summary>
        Task<UserDetails?> GetUserDetails(Guid userId);

        /// <summary>
        /// Returns all the UserDetails from the database
        /// </summary>
        Task<List<UserDetails>> GetAllUserDetails();

        /// <summary>
        /// Returns all the UserIds from the database
        /// </summary>
        Task<List<string>> GetAllUserDetailIds();

        /// <summary>
        /// Returns the Full Name for a given user.
        /// </summary>
        string GetFullName(Guid userId);

    }
}