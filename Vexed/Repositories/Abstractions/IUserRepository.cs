using Microsoft.AspNetCore.Identity;
using Vexed.Models;
using Vexed.Models.ViewModels;

namespace Vexed.Repositories.Abstractions
{
    public interface IUserRepository : IRepositoryBase<IdentityUser>
    {
        /// <summary>
        /// Returns the Guid of the superior for the given user.
        /// </summary>
        Guid GetUserSuperior(Guid userId);

        /// <summary>
        /// Returns all the identity users
        /// </summary>
        List<IdentityUser> GetAllUsers();

        /// <summary>
        /// Returns all the UserIds and UserNames of the users that are 
        /// </summary>
        List<UserNameVM> GetUnassignedUserNames();
    }
}
