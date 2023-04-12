using Microsoft.EntityFrameworkCore;
using Shared;
using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class UserDetailsRepository : RepositoryBase<UserDetails>, IUserDetailsRepository
    {
        private Logger _logger;
        public UserDetailsRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<List<string>> GetAllUserDetailIds()
        {
            try
            {
                return await _vexedDbContext.UsersDetails.Select(u => u.UserId.ToString().ToLower()).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserDetails>> GetAllUserDetails()
        {
            try
            {
                return await _vexedDbContext.UsersDetails.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserDetails>> GetUserDetails(Guid userId)
        {
            try
            {
                return await _vexedDbContext.UsersDetails.Where(u => u.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserDetails> GetUserDetailsById(int id)
        {
            try
            {
                return await _vexedDbContext.UsersDetails.Where(u => u.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserDetails> GetUserDetailsByUserId(Guid userId)
        {
            try
            {
                return await _vexedDbContext.UsersDetails.Where(u => u.UserId == userId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
