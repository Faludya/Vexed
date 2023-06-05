using Microsoft.EntityFrameworkCore;
using Shared;
using DataAccess;
using DataModels;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class EmergencyContactRepository : RepositoryBase<EmergencyContact>, IEmergencyContactRepository
    {
        private Logger _logger;
        public EmergencyContactRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<EmergencyContact> GetEmergencyContactById(int id)
        {
            try
            {
                return await _vexedDbContext.EmergencyContacts.Where(e => e.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<EmergencyContact>> GetEmergencyContacts(Guid userId)
        {
            try
            {
                return await _vexedDbContext.EmergencyContacts.Where(e => e.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
