using Microsoft.EntityFrameworkCore;
using Shared;
using DataAccess;
using DataModels;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class UserEmploymentRepository : RepositoryBase<UserEmployment>, IUserEmploymentRepository
    {
        private readonly Logger _logger;
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

        public async Task<List<UserEmployment>> GetTeamMembersEmployment(Guid superiorId)
        {
            try
            {
                return await _vexedDbContext.UsersEmployments.Where(u => u.SuperiorId == superiorId && u.UserId != superiorId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserEmployment?> GetUserEmploymentById(int id)
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

        public async Task<UserEmployment?> GetUserEmploymentByUserId(Guid userId)
        {
            try
            {
                return await _vexedDbContext.UsersEmployments.FirstOrDefaultAsync(u => u.UserId == userId);
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

        public async Task<UserEmployment?> GetUserSuperior(Guid userId)
        {
            try
            {
                var currUser = await _vexedDbContext.UsersEmployments.Where(e => e.UserId == userId).FirstOrDefaultAsync();
                if (currUser != null)
                {
                    var superiorEmployment = await GetUserEmploymentByUserId(currUser.SuperiorId);
                    if (superiorEmployment != null)
                    {
                        return superiorEmployment;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

    }
}
