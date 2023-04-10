using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequest>
    {
        /// <summary>
        /// Returns the first Leave Request with the given <c>id</c>
        /// </summary>
        LeaveRequest GetLeaveRequestById(int id);

        /// <summary>
        /// Returns all the Leave Requests for a given user.
        /// </summary>
        List<LeaveRequest> GetLeaveRequests(Guid userId);

        /// <summary>
        /// Returns all the Leave Requests for a given superior.
        /// </summary>
        List<LeaveRequest> GetLeaveRequestsSuperior(Guid superiorId);
    }
}
