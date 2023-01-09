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
    }
}
