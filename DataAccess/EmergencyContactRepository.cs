using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class EmergencyContactRepository : RepositoryBase<EmergencyContact>, IEmergencyContactRepository
    {
        public EmergencyContactRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }

        public EmergencyContact GetEmergencyContactById(int id)
        {
            return _vexedDbContext.EmergencyContacts.Where(e => e.Id == id).First();
        }

        public List<EmergencyContact> GetEmergencyContacts(Guid userId)
        {
            return _vexedDbContext.EmergencyContacts.Where(e => e.UserId == userId).ToList();
        }
    }
}
