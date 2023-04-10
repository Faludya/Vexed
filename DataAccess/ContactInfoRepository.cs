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

        public ContactInfo GetContactInfoById(int id)
        {
            return _vexedDbContext.ContactsInfo.Where(c => c.Id == id).First();
        }

        public List<ContactInfo> GetContactInfos(Guid userId)
        {
            return _vexedDbContext.ContactsInfo.Where(c => c.UserId == userId).ToList();
        }
    }
}
