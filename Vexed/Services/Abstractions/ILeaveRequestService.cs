using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface ILeaveRequestService
    {
        /// <summary>
        /// Creates a new Leave Request.
        /// </summary>
        void CreateLeaveRequest(LeaveRequest leaveRequest);

        /// <summary>
        /// Updates the details of a Leave Request.
        /// </summary>
        void UpdateLeaveRequest(LeaveRequest leaveRequest);

        /// <summary>
        /// Removes the Leave Request from the  database.
        /// </summary>
        void DeleteLeaveRequest(LeaveRequest leaveRequest);

        /// <summary>
        /// Returns the first Leave Request with the given <c>id</c>
        /// </summary>
        LeaveRequest GetLeaveRequestById(int id);

        /// <summary>
        /// Returns all Leave requests from the database.
        /// </summary>
        List<LeaveRequest> GetAllLeaveRequests();

        /// <summary>
        /// Returns all the Leave Requests for a given user.
        /// </summary>
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
