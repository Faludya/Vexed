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

        public UserEmployment GetUserEmploymentById(int id)
        {
            return _vexedDbContext.UsersEmployments.Where(u => u.Id == id).First();
        }
    }
}
