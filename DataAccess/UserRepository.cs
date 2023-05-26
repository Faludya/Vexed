using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using DataAccess;
using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Shared.ViewModels.CombinedClasses;

namespace Vexed.Repositories
{
    public class UserRepository : RepositoryBase<IdentityUser>, IUserRepository
    {
        private Logger _logger;
        public UserRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<List<string>> GetAllUserIds()
        {
            try
            {
                return await _vexedDbContext.Users.Select(i => i.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserNameVM>> GetAllUserNames()
        {
            try
            {
                var users = await _vexedDbContext.Users
                        .Select(u => new UserNameVM { UserId = u.Id, UserName = u.UserName })
                        .ToListAsync(); 

                return await _vexedDbContext.Users
                        .Select(u => new UserNameVM { UserId = u.Id, UserName = u.UserName })
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            try
            {
                return await _vexedDbContext.Users.Include(u => u.UserName).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserNameVM>> GetUnsusedUserNames(List<string> unusedUserIds)
        {
            try
            {
                return await _vexedDbContext.Users
                    .Where(u => unusedUserIds.Contains(u.Id))
                    .Select(u => new UserNameVM { UserId = u.Id, UserName = u.UserName })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<string> GetUserName(string userId)
        {
            try
            {
                return await _vexedDbContext.Users.Where(u => u.Id == userId).Select(u => u.UserName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<Guid> GetUserSuperior(Guid userId)
        {
            try
            {
                return await _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).Select(u => u.SuperiorId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
