using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    internal class UserDetailsRepository : RepositoryBase<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }
    }
}
