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
        /// Returns all the User Details for a given user.
        /// </summary>
        List<UserDetails> GetUserDetails(Guid userId);
    }
}