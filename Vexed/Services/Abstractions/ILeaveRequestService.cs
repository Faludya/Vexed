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
        List<LeaveRequest> GetLeaveRequests(Guid userId);

        /// <summary>
        /// Method <c>GetLeaveTypes</c> returns a list of all Leave types.
        /// </summary>
        List<string> GetLeaveTypes();

        /// <summary>
        /// Method <c>GetLeaveTypes</c> returns a list of all Leave types and moves the selected type
        /// to the first position.
        /// </summary>
        List<string> GetLeaveTypes(string selectedLeave);
    }
}
