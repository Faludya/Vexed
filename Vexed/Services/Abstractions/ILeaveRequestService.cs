using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface ILeaveRequestService
    {
        void CreateLeaveRequest(LeaveRequest leaveRequest);
        void UpdateLeaveRequest(LeaveRequest leaveRequest);
        void DeleteLeaveRequest(LeaveRequest leaveRequest);
        LeaveRequest GetLeaveRequestById(int id);
        List<LeaveRequest> GetAllLeaveRequests();
    }
}
