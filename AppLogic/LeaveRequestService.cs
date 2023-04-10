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
            leaveRequest.SuperiorId = _repositoryWrapper.UserRepository.GetUserSuperior(leaveRequest.UserId);

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

        public List<string> GetLeaveTypes()
        {
            var contactTypes = new List<string>()
            {
                "Annual Leave", "Medical Leave", "Maternity Leave", "Special Leave", "Unpaid Leave", "TOIL Travel"
            };

            return contactTypes;
        }

        public List<string> GetLeaveTypes(string selectedLeave)
        {
            var leaveTypes = new List<string>()
            {
                "Annual Leave", "Medical Leave", "Maternity Leave", "Special Leave", "Unpaid Leave", "TOIL Travel"
            };
            int posSelected = leaveTypes.IndexOf(selectedLeave);
            (leaveTypes[0], leaveTypes[posSelected]) = (leaveTypes[posSelected], leaveTypes[0]);

            return leaveTypes;
        }

        public List<LeaveRequest> GetLeaveRequestsForSuperior(Guid superiorId)
        {
            return _repositoryWrapper.LeaveRequestRepository.GetLeaveRequestsSuperior(superiorId);
        }
    }
}
