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

        public List<string> GetAllUserIds()
        {
            return _vexedDbContext.Users.Select(i => i.Id).ToList();
        }

        public List<UserNameVM> GetAllUserNames()
        {
            return _vexedDbContext.Users
                    .Select(u => new UserNameVM { UserId = u.Id, UserName = u.UserName })
                    .ToList();
        }

        public List<IdentityUser> GetAllUsers()
        {
            return _vexedDbContext.Users.Include(u => u.UserName).ToList();
        }

        public List<UserNameVM> GetUnsusedUserNames(List<string> unusedUserIds)
        {
            var userNames = _vexedDbContext.Users
                .Where(u => unusedUserIds.Contains(u.Id))
                .Select(u => new UserNameVM { UserId = u.Id, UserName = u.UserName })
                .ToList();

            return userNames;
        }

        public string GetUserName(string userId)
        {
            return _vexedDbContext.Users.Where(u => u.Id == userId).Select(u => u.UserName).First();
        }

        public Guid GetUserSuperior(Guid userId)
        {
            return _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).Select(u => u.SuperiorId).FirstOrDefault();
        }
    }
}
