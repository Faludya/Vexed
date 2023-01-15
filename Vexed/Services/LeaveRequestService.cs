using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public LeaveRequestService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public void CreateLeaveRequest(LeaveRequest leaveRequest)
        {
            _repositoryWrapper.LeaveRequestRepository.Create(leaveRequest);
            _repositoryWrapper.Save();
        }

        public void DeleteLeaveRequest(LeaveRequest leaveRequest)
        {
            _repositoryWrapper.LeaveRequestRepository.Delete(leaveRequest);
            _repositoryWrapper.Save();
        }

        public List<LeaveRequest> GetAllLeaveRequests()
        {
            return _repositoryWrapper.LeaveRequestRepository.FindAll().ToList();
        }

        public LeaveRequest GetLeaveRequestById(int id)
        {
            return _repositoryWrapper.LeaveRequestRepository.GetLeaveRequestById(id);
        }

        public List<LeaveRequest> GetLeaveRequests(Guid userId)
        {
            return _repositoryWrapper.LeaveRequestRepository.GetLeaveRequests(userId);
        }

        public void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            _repositoryWrapper.LeaveRequestRepository.Update(leaveRequest);
            _repositoryWrapper.Save();
        }
    }
}
