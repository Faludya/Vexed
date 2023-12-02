using Shared;
using DataModels;
using DataModels.ViewModels;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly Logger _logger;

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
                leaveRequest.TotalHours = ComputeTotalLeaveHours(leaveRequest);

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
                leaveRequest.TotalHours = ComputeTotalLeaveHours(leaveRequest);

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
            return GetLeaveTypesList();
        }

        public List<string> GetLeaveTypes(string selectedLeave)
        {
            try
            {
                var leaveTypes = GetLeaveTypesList();
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
                // Get User team members
                var team = await _repositoryWrapper.ProjectTeamRepository.GetUserProjectTeam(userId);
                team = team.OrderByDescending(t => t.StartDate).ToList();

                var project = team[0].Project;
                var projectTeams = await _repositoryWrapper.ProjectTeamRepository.GetUserProjectTeam(project.Id);
                // Get time cards for each member
                var teamLeaves = projectTeams
                    .Select(async member =>
                    {
                        var userName = _repositoryWrapper.UserDetailsRepository.GetFullName(member.UserId);
                        var leaves = await _repositoryWrapper.LeaveRequestRepository.GetLeaveRequests(member.UserId);

                        return new TeamLeaveRequestVM
                        {
                            UserName = userName,
                            Leaves = leaves,
                        };
                    })
                    .Select(task => task.Result)
                    .ToList();


                return teamLeaves;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public static float ComputeTotalLeaveHours(LeaveRequest leaveRequest)
        {
            TimeSpan duration = leaveRequest.EndDate - leaveRequest.StartDate;
            int totalDays = duration.Days + 1;
            float totalHours = leaveRequest.Quantity * totalDays;

            return totalHours;
        }

        public static List<string> GetLeaveTypesList()
        {
            return new List<string>()
                {
                    "Annual Leave", "Medical Leave", "Maternity Leave", "Special Leave", "Unpaid Leave", "TOIL Travel"
                };
        }
    }
}
