using Microsoft.EntityFrameworkCore;
using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class ContactInfoRepository : RepositoryBase<ContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }

        public async Task<ContactInfo> GetContactInfoById(int id)
        {
            return await _vexedDbContext.ContactsInfo.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ContactInfo>> GetContactInfos(Guid userId)
        {
            return await _vexedDbContext.ContactsInfo.Where(c => c.UserId == userId).ToListAsync();
        }
    }
}
