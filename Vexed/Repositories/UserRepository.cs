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

        public List<IdentityUser> GetAllUsers()
        {
            return _vexedDbContext.Users.Include(u => u.UserName).ToList();
        }

        public List<UserNameVM> GetUnassignedUserNames()
        {
            var users = _vexedDbContext.Users.ToList();
            List<UserNameVM> userNameVM = new List<UserNameVM>();
            foreach(var user in users)
            {
                var userName = new UserNameVM();
                userName.UserName = user.UserName;
                userName.UserId = Guid.Parse(user.Id);
                userNameVM.Add(userName);
            }
            return userNameVM;
        }

        public Guid GetUserSuperior(Guid userId)
        {
            return _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).Select(u => u.SuperiorId).FirstOrDefault();
        }
    }
}
