using DataModels.ViewModels;
using DataModels;

namespace Vexed.Services.Abstractions
{
    public interface ILeaveRequestService
    {
        /// <summary>
        /// Creates a new Leave Request.
        /// </summary>
        Task CreateLeaveRequest(LeaveRequest leaveRequest);

        /// <summary>
        /// Updates the details of a Leave Request.
        /// </summary>
        Task UpdateLeaveRequest(LeaveRequest leaveRequest);

        /// <summary>
        /// Removes the Leave Request from the  database.
        /// </summary>
        Task DeleteLeaveRequest(LeaveRequest leaveRequest);

        /// <summary>
        /// Returns the first Leave Request with the given <c>id</c>
        /// </summary>
        Task<LeaveRequest> GetLeaveRequestById(int id);

        /// <summary>
        /// Returns all Leave requests from the database ordered for HR
        /// </summary>
        Task<List<UserLeaveRequestsViewModel>> GetLeaveRequestsHR();

        /// <summary>
        /// Returns all the Leave Requests for a given user.
        /// </summary>
        Task<List<LeaveRequest>> GetLeaveRequests(Guid userId);

        /// <summary>
        /// Returns all the Leave Requests for a given superior.
        /// </summary>
        Task<List<LeaveRequest>> GetLeaveRequestsForSuperior(Guid superiorId);

        /// <summary>
        /// Method <c>GetLeaveTypes</c> returns a list of all Leave types.
        /// </summary>
        List<string> GetLeaveTypes();

        /// <summary>
        /// Method <c>GetLeaveTypes</c> returns a list of all Leave types and moves the selected type
        /// to the first position.
        /// </summary>
        List<string> GetLeaveTypes(string selectedLeave);

        /// <summary>
        /// Returns all the Leave Requests for a given user.
        /// </summary>
        Task<List<TeamLeaveRequestVM>> GetTeamLeaveRequests(Guid userId);
    }
}
