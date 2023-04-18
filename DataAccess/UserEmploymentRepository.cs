using Microsoft.EntityFrameworkCore;
using Shared;
using DataAccess;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class UserEmploymentRepository : RepositoryBase<UserEmployment>, IUserEmploymentRepository
    {
        private Logger _logger;
        public UserEmploymentRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<List<string>> GetAllUserEmploymentIds()
        {
            try
            {
            return await _vexedDbContext.UsersEmployments.Select(u => u.UserId.ToString().ToLower()).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserEmployment> GetUserEmploymentById(int id)
        {
            try
            {
                return await _vexedDbContext.UsersEmployments.Where(u => u.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserEmployment> GetUserEmploymentByUserId(Guid userId)
        {
            try
            {
                return await _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserEmployment>> GetUserEmployments(Guid userId)
        {
            try
            {
                return await _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
