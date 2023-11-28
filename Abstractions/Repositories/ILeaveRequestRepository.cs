using DataModels.ViewModels;
using DataModels;

namespace Vexed.Repositories.Abstractions
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequest>
    {
        /// <summary>
        /// Returns the first Leave Request with the given <c>id</c>
        /// </summary>
        Task<LeaveRequest> GetLeaveRequestById(int id);

        /// <summary>
        /// Returns all the Leave Requests for a given user.
        /// </summary>
        Task<List<LeaveRequest>> GetLeaveRequests(Guid userId);

        /// <summary>
        /// Returns all the Leave Requests for a given superior.
        /// </summary>
        Task<List<UserLeaveRequestsViewModel>> GetLeaveRequestsSuperior(Guid superiorId);

        /// <summary>
        /// Returns all the Leave Requests sorted by date and status
        /// </summary>
        Task<List<UserLeaveRequestsViewModel>> GetLeaveRequestsHR();

        /// <summary>
        /// Returns the total hours of paid leaves for the user
        /// </summary>
        float GetLeaveHours(Guid userId);

        /// <summary>
        /// Returns the total days of paid leaves for the user
        /// </summary>
        int GetLeaveDays(Guid userId);
    }
}
