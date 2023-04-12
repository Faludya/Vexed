using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vexed.Data;
using Vexed.Models;
using Vexed.Models.ViewModels;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class UserRepository : RepositoryBase<IdentityUser>, IUserRepository
    {
        public UserRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }

        public async Task<List<string>> GetAllUserIds()
        {
            return await _vexedDbContext.Users.Select(i => i.Id).ToListAsync();
        }

        public async Task<List<UserNameVM>> GetAllUserNames()
        {
            return await _vexedDbContext.Users
                        .Select(u => new UserNameVM { UserId = u.Id, UserName = u.UserName })
                        .ToListAsync();
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            return await _vexedDbContext.Users.Include(u => u.UserName).ToListAsync();
        }

        public async Task<List<UserNameVM>> GetUnsusedUserNames(List<string> unusedUserIds)
        {
            return await _vexedDbContext.Users
                .Where(u => unusedUserIds.Contains(u.Id))
                .Select(u => new UserNameVM { UserId = u.Id, UserName = u.UserName })
                .ToListAsync();
        }

        public async Task<string> GetUserName(string userId)
        {
            return await _vexedDbContext.Users.Where(u => u.Id == userId).Select(u => u.UserName).FirstOrDefaultAsync();
        }

        public async Task<Guid> GetUserSuperior(Guid userId)
        {
            return await _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).Select(u => u.SuperiorId).FirstOrDefaultAsync();
        }
    }
}
