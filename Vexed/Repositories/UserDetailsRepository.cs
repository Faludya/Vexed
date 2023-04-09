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

        public List<string> GetAllUserDetailIds()
        {
            return _vexedDbContext.UsersDetails.Select(u => u.UserId.ToString().ToLower()).ToList();
        }

        public List<UserDetails> GetAllUserDetails()
        {
            return _vexedDbContext.UsersDetails.ToList();
        }

        public List<UserDetails> GetUserDetails(Guid userId)
        {
            return _vexedDbContext.UsersDetails.Where(u => u.UserId == userId).ToList();
        }

        public UserDetails GetUserDetailsById(int id)
        {
            return _vexedDbContext.UsersDetails.Where(u => u.Id == id).First();
        }

        public UserDetails GetUserDetailsByUserId(Guid userId)
        {
            return _vexedDbContext.UsersDetails.Where(u => u.UserId == userId).First();
        }
    }
}
