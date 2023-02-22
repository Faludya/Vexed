using Microsoft.AspNetCore.Identity;
using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface IUserRepository : IRepositoryBase<IdentityUser>
    {
        /// <summary>
        /// Returns the Guid of the superior for the given user.
        /// </summary>
        Guid GetUserSuperior(Guid userId);
    }
}
