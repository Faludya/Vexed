using Microsoft.EntityFrameworkCore;
using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class UserDetailsRepository : RepositoryBase<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }

        public async Task<List<string>> GetAllUserDetailIds()
        {
            return await _vexedDbContext.UsersDetails.Select(u => u.UserId.ToString().ToLower()).ToListAsync();
        }

        public async Task<List<UserDetails>> GetAllUserDetails()
        {
            return await _vexedDbContext.UsersDetails.ToListAsync();
        }

        public async Task<List<UserDetails>> GetUserDetails(Guid userId)
        {
            return await _vexedDbContext.UsersDetails.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<UserDetails> GetUserDetailsById(int id)
        {
            return await _vexedDbContext.UsersDetails.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserDetails> GetUserDetailsByUserId(Guid userId)
        {
            return await _vexedDbContext.UsersDetails.Where(u => u.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
