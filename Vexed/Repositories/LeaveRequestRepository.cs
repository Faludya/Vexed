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

        public LeaveRequest GetLeaveRequestById(int id)
        {
            return _vexedDbContext.LeaveRequests.Where(l => l.Id == id).First();
        }
    }
}
