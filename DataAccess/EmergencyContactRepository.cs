using Microsoft.EntityFrameworkCore;
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

        public async Task<EmergencyContact> GetEmergencyContactById(int id)
        {
            return await _vexedDbContext.EmergencyContacts.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<EmergencyContact>> GetEmergencyContacts(Guid userId)
        {
            return await _vexedDbContext.EmergencyContacts.Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
