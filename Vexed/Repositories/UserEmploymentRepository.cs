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

        public List<string> GetAllUserEmploymentIds()
        {
            return _vexedDbContext.UsersEmployments.Select(u => u.UserId.ToString().ToLower()).ToList();
        }

        public UserEmployment GetUserEmploymentById(int id)
        {
            return _vexedDbContext.UsersEmployments.Where(u => u.Id == id).First();
        }

        public UserEmployment GetUserEmploymentByUserId(Guid userId)
        {
            return _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).First();
        }

        public List<UserEmployment> GetUserEmployments(Guid userId)
        {
            return _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).ToList();
        }
    }
}
