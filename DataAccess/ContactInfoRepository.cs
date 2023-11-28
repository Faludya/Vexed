using DataAccess;
using Microsoft.EntityFrameworkCore;
using Shared;
using DataModels;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class ContactInfoRepository : RepositoryBase<ContactInfo>, IContactInfoRepository
    {
        private readonly Logger _logger;
        public ContactInfoRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<ContactInfo> GetContactInfoById(int id)
        {
            try
            {
                return await _vexedDbContext.ContactsInfo!.Where(c => c.Id == id).FirstAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<ContactInfo>> GetContactInfos(Guid userId)
        {
            try
            {
                return await _vexedDbContext.ContactsInfo!.Where(c => c.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
