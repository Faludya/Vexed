﻿using Shared;
using DataModels;
using DataModels.ViewModels;
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
                TimeSpan duration = leaveRequest.EndDate - leaveRequest.StartDate;
                int totalDays = duration.Days + 1;
                leaveRequest.TotalHours = leaveRequest.Quantity * totalDays;

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
                TimeSpan duration = leaveRequest.EndDate - leaveRequest.StartDate;
                int totalDays = duration.Days + 1;
                leaveRequest.TotalHours = leaveRequest.Quantity * totalDays;

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

        public async Task<List<UserLeaveRequestsViewModel>> GetLeaveRequestsForSuperior(Guid superiorId)
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

        public async Task<List<TeamLeaveRequestVM>> GetTeamLeaveRequests(Guid userId)
        {
            try
            {
                //Get User team members
                var team = await _repositoryWrapper.ProjectTeamRepository.GetUserProjectTeam(userId);
                team.OrderByDescending(t => t.StartDate);
                var project = team[0].Project;
                var projectTeams = await _repositoryWrapper.ProjectTeamRepository.GetUserProjectTeam(project.Id);
                var teamLeaves = new List<TeamLeaveRequestVM>();

                //Get time cards for each member
                foreach(var member in projectTeams)
                {
                    var userName = _repositoryWrapper.UserDetailsRepository.GetFullName(member.UserId);
                    var leaves = await _repositoryWrapper.LeaveRequestRepository.GetLeaveRequests(member.UserId);

                    var teamLR = new TeamLeaveRequestVM
                    {
                        UserName = userName,
                        Leaves = leaves,
                    };

                    teamLeaves.Add(teamLR);
                }

                return teamLeaves;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
