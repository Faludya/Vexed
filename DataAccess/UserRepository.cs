using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using DataAccess;
using Vexed.Repositories.Abstractions;
using DataModels.ViewModels;

namespace Vexed.Repositories
{
    public class UserRepository : RepositoryBase<IdentityUser>, IUserRepository
    {
        private Logger _logger;
        private UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(VexedDbContext vexedDbContext, Logger logger, UserManager<IdentityUser> userManager, 
                                RoleManager<IdentityRole> roleManager) : base(vexedDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateUserRole(Guid userId, string roleName)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var roleExists = await _roleManager.RoleExistsAsync(roleName);

                if(roleExists)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                _vexedDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
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

        public async Task<List<string>> GetAllUserRoles()
        {
            var roles = await _vexedDbContext.Roles.Select(r => r.Name).ToListAsync();

            return roles;
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

        public async Task<List<string>> GetUserRoles(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var userRoles = (List<string>)await _userManager.GetRolesAsync(user);

            return userRoles;
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

        public async Task UpdateUserRoles(Guid userId, List<string> selectedRoles)
        {
            try
            {
                await DeleteUserRoles(userId);
                
                foreach (var role in selectedRoles)
                {
                    await CreateUserRole(userId, role);
                }

                _vexedDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteUserRoles(Guid userId)
        {
            try
            {
                var userRoles = _vexedDbContext.UserRoles.Where(ur => ur.UserId == userId.ToString());
                _vexedDbContext.UserRoles.RemoveRange(userRoles);
                _vexedDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
