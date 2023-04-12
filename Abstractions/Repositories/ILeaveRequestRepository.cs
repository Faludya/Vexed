using Vexed.Models;

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
        Task<List<LeaveRequest>> GetLeaveRequestsSuperior(Guid superiorId);
    }
}
