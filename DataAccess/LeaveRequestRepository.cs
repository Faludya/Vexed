using Microsoft.EntityFrameworkCore;
using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class LeaveRequestRepository : RepositoryBase<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }

        public async Task<LeaveRequest> GetLeaveRequestById(int id)
        {
            return await _vexedDbContext.LeaveRequests.Where(l => l.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequests(Guid userId)
        {
            return await _vexedDbContext.LeaveRequests.Where(l => l.UserId == userId).ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsSuperior(Guid superiorId)
        {
            return await _vexedDbContext.LeaveRequests.Where(l => l.SuperiorId == superiorId).ToListAsync();
        }
    }
}
