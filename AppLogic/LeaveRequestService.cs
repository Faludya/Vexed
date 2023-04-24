using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.ViewModels;
using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private Logger _logger;

        public LeaveRequestService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                leaveRequest.SuperiorId = await _repositoryWrapper.UserRepository.GetUserSuperior(leaveRequest.UserId);

                await _repositoryWrapper.LeaveRequestRepository.Create(leaveRequest);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                await _repositoryWrapper.LeaveRequestRepository.Delete(leaveRequest);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserLeaveRequestsViewModel>> GetLeaveRequestsHR()
        {
            try
            {
                return await _repositoryWrapper.LeaveRequestRepository.GetLeaveRequestsHR();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<LeaveRequest> GetLeaveRequestById(int id)
        {
            try
            {
                return await _repositoryWrapper.LeaveRequestRepository.GetLeaveRequestById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<LeaveRequest>> GetLeaveRequests(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.LeaveRequestRepository.GetLeaveRequests(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                await _repositoryWrapper.LeaveRequestRepository.Update(leaveRequest);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public List<string> GetLeaveTypes()
        {
            try
            {
                var contactTypes = new List<string>()
                {
                    "Annual Leave", "Medical Leave", "Maternity Leave", "Special Leave", "Unpaid Leave", "TOIL Travel"
                };

                return contactTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public List<string> GetLeaveTypes(string selectedLeave)
        {
            try
            {
                var leaveTypes = new List<string>()
                {
                    "Annual Leave", "Medical Leave", "Maternity Leave", "Special Leave", "Unpaid Leave", "TOIL Travel"
                };
                int posSelected = leaveTypes.IndexOf(selectedLeave);
                (leaveTypes[0], leaveTypes[posSelected]) = (leaveTypes[posSelected], leaveTypes[0]);

                return leaveTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsForSuperior(Guid superiorId)
        {
            try
            {
                return await _repositoryWrapper.LeaveRequestRepository.GetLeaveRequestsSuperior(superiorId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
