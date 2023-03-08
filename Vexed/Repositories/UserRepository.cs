using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vexed.Services.Abstractions;

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

        public List<string> GetUserUsernames()
        {
            return new List<string>();
            //var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        }
    }
}
