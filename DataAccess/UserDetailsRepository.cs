using Microsoft.EntityFrameworkCore;
using Shared;
using DataAccess;
using DataModels;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class UserDetailsRepository : RepositoryBase<UserDetails>, IUserDetailsRepository
    {
        private readonly Logger _logger;
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

        public string GetFullName(Guid userId)
        {
            try
            {
                var firstName = _vexedDbContext.UsersDetails.Where(u => u.UserId == userId).Select(u => u.FirstName).FirstOrDefault();
                var LastName = _vexedDbContext.UsersDetails.Where(u => u.UserId == userId).Select(u => u.LastName).FirstOrDefault();
                var fullName = firstName + " " + LastName;
                return fullName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserDetails> GetUserDetails(Guid userId)
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
