using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    internal class EmergencyContactRepository : RepositoryBase<EmergencyContact>, IEmergencyContactRepository
    {
        public EmergencyContactRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }

        public EmergencyContact GetEmergencyContactById(int id)
        {
            return _vexedDbContext.EmergencyContacts.Where(e => e.Id == id).First();
        }
    }
}
