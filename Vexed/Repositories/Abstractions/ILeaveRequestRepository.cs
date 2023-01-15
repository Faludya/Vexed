using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequest>
    {
        LeaveRequest GetLeaveRequestById(int id);
        List<LeaveRequest> GetLeaveRequests(Guid userId);
    }
}
