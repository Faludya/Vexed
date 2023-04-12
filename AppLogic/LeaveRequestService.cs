using Microsoft.EntityFrameworkCore;
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

        public async Task CreateLeaveRequest(LeaveRequest leaveRequest)
        {
            leaveRequest.SuperiorId = await _repositoryWrapper.UserRepository.GetUserSuperior(leaveRequest.UserId);

            await _repositoryWrapper.LeaveRequestRepository.Create(leaveRequest);
            await _repositoryWrapper.Save();
        }

        public async Task DeleteLeaveRequest(LeaveRequest leaveRequest)
        {
            await _repositoryWrapper.LeaveRequestRepository.Delete(leaveRequest);
            await _repositoryWrapper.Save();
        }

        public async Task<List<LeaveRequest>> GetAllLeaveRequests()
        {
            var queryable = await _repositoryWrapper.LeaveRequestRepository.FindAll();
            return await queryable.ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestById(int id)
        {
            return await _repositoryWrapper.LeaveRequestRepository.GetLeaveRequestById(id);
        }

        public async Task<List<LeaveRequest>> GetLeaveRequests(Guid userId)
        {
            return await _repositoryWrapper.LeaveRequestRepository.GetLeaveRequests(userId);
        }

        public async Task UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            await _repositoryWrapper.LeaveRequestRepository.Update(leaveRequest);
            await _repositoryWrapper.Save();
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

        public async Task<List<LeaveRequest>> GetLeaveRequestsForSuperior(Guid superiorId)
        {
            return await _repositoryWrapper.LeaveRequestRepository.GetLeaveRequestsSuperior(superiorId);
        }
    }
}
