using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    internal class UserEmploymentRepository : RepositoryBase<UserEmployment>, IUserEmploymentRepository
    {
        public UserEmploymentRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }
    }
}
