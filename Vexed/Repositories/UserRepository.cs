using Microsoft.AspNetCore.Identity;
using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class UserRepository : RepositoryBase<IdentityUser>, IUserRepository
    {
        public UserRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }

        public Guid GetUserSuperior(Guid userId)
        {
            return _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).Select(u => u.SuperiorId).FirstOrDefault();
        }
    }
}
