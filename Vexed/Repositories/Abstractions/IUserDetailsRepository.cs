using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface IUserDetailsRepository : IRepositoryBase<UserDetails>
    {
        /// <summary>
        /// Returns the first User Detail with the given <c>id</c>
        /// </summary>
        UserDetails GetUserDetailsById(int id);

        /// <summary>
        /// Returns the first User Detail with the given <c>userId</c>
        /// </summary>
        UserDetails GetUserDetailsByUserId(Guid userId);

        /// <summary>
        /// Returns all the User Details for a given user.
        /// </summary>
        List<UserDetails> GetUserDetails(Guid userId);
    }
}