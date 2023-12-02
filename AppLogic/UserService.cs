using Microsoft.AspNetCore.Identity;
using Shared;
using DataModels.ViewModels;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;
using DataModels;
using System.Linq;
using System.Threading.Tasks;

namespace Vexed.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly Logger _logger;

        public UserService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<List<UserNameVM>> GetAllUserNames()
        {
            try
            {
                return await _repositoryWrapper.UserRepository.GetAllUserNames();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            try
            {
                return await _repositoryWrapper.UserRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserNameVM>> GetUnassignedUserDetails()
        {
            try
            {
                var allUsersIds = await _repositoryWrapper.UserRepository.GetAllUserIds();
                var usedIds = await _repositoryWrapper.UserDetailsRepository.GetAllUserDetailIds();
                var unusedUserIds = allUsersIds.Except(usedIds).ToList();

                return await _repositoryWrapper.UserRepository.GetUnsusedUserNames(unusedUserIds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserNameVM>> GetUnassignedUserEmployment()
        {
            try
            {
                var allUsersIds = await _repositoryWrapper.UserRepository.GetAllUserIds();
                var usedIds = await _repositoryWrapper.UserEmploymentRepository.GetAllUserEmploymentIds();
                var unusedUserIds = allUsersIds.Except(usedIds).ToList();

                return await _repositoryWrapper.UserRepository.GetUnsusedUserNames(unusedUserIds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<string?> GetUserName(string userId)
        {
            try
            {
                return await _repositoryWrapper.UserRepository.GetUserName(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public OrganizationChartViewModel GetOrganizationChart(Guid userId)
        {
            try
            {
                var orgChar = new OrganizationChartViewModel
                {
                    TeamMembers = new List<UserInfoVM>()
                };

                var superiorProfile = GetSuperiorProfile(userId);
                orgChar.Superior = superiorProfile;

                var teamEmployments = _repositoryWrapper.UserEmploymentRepository.GetTeamMembersEmployment(superiorProfile.Employment.UserId).Result;

                foreach (var member in teamEmployments)
                {
                    var memberProfile = GetMemberProfile(member.UserId);
                    orgChar.TeamMembers.Add(memberProfile);
                }

                return orgChar;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        private UserInfoVM GetSuperiorProfile(Guid userId)
        {
            var superiorEmployment = _repositoryWrapper.UserEmploymentRepository.GetUserSuperior(userId).Result ?? new UserEmployment();
            var superiorDetails = _repositoryWrapper.UserDetailsRepository.GetUserDetails(superiorEmployment.UserId).Result ?? new UserDetails();
            var superiorEmail = _repositoryWrapper.UserRepository.GetUserName(superiorEmployment.UserId.ToString()).Result ?? string.Empty;

            return new UserInfoVM()
            {
                Employment = superiorEmployment,
                Details = superiorDetails,
                Email = superiorEmail
            };
        }

        private UserInfoVM GetMemberProfile(Guid memberId)
        {
            var userDetails = _repositoryWrapper.UserDetailsRepository.GetUserDetails(memberId).Result ?? new UserDetails();
            var userEmail = _repositoryWrapper.UserRepository.GetUserName(memberId.ToString()).Result ?? string.Empty;

            return new UserInfoVM()
            {
                Employment = _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentByUserId(memberId).Result ?? new UserEmployment(),
                Details = userDetails,
                Email = userEmail
            };
        }


        public async Task<List<string>> GetAllUserRoles()
        {
            try
            {
                return await _repositoryWrapper.UserRepository.GetAllUserRoles();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<string>> GetUserRoles(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.UserRepository.GetUserRoles(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task CreateUserRole(Guid userId, string roleName)
        {
            try
            {
                await _repositoryWrapper.UserRepository.CreateUserRole(userId, roleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateUserRoles(Guid userId, List<string> selectedRoles)
        {
            try
            {
                await _repositoryWrapper.UserRepository.UpdateUserRoles(userId, selectedRoles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<CardsVM>> GetLastCards(Guid userId)
        {
            try
            {
                var timeCards = await GetTopEntries(_repositoryWrapper.TimeCardRepository.GetTimeCards(userId));
                var leaveRequests = await GetTopEntries(_repositoryWrapper.LeaveRequestRepository.GetLeaveRequests(userId));

                var combinedEntries = CombineEntries(timeCards, leaveRequests);

                // Sort the combined entries based on timestamp or other relevant criteria
                combinedEntries.Sort((entry1, entry2) => entry2.StartDate.CompareTo(entry1.StartDate));

                // Select the necessary fields for display
                var displayEntries = combinedEntries.Take(10).ToList();

                return displayEntries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        private static async Task<List<CardsVM>> GetTopEntries(Task<List<TimeCard>> entriesTask)
        {
            var entries = await entriesTask;
            entries.Sort((t1, t2) => t2.StartDate.CompareTo(t2.StartDate));
            return entries.Take(10).Select(timeCard =>
                new CardsVM
                {
                    Name = timeCard.ProjectCode,
                    Status = timeCard.Status!,
                    StartDate = timeCard.StartDate,
                    EndDate = timeCard.EndDate
                }).ToList();
        }

        private static async Task<List<CardsVM>> GetTopEntries(Task<List<LeaveRequest>> entriesTask)
        {
            var entries = await entriesTask;
            entries.Sort((l1, l2) => l2.StartDate.CompareTo(l1.StartDate));
            return entries.Take(10).Select(leaveRequest =>
                new CardsVM
                {
                    Name = leaveRequest.Type,
                    Status = leaveRequest.Status!,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate
                }).ToList();
        }

        private static List<CardsVM> CombineEntries(List<CardsVM> timeCards, List<CardsVM> leaveRequests)
        {
            var combinedEntries = new List<CardsVM>();
            combinedEntries.AddRange(timeCards);
            combinedEntries.AddRange(leaveRequests);
            return combinedEntries;
        }


        public async Task<UserProfileVM> GetUserProfile(Guid userId)
        {
            try
            {
                var userProfile = new UserProfileVM
                {
                    Employment = await _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentByUserId(userId) ?? new UserEmployment(),
                    ProjectTeams = await _repositoryWrapper.ProjectTeamRepository.GetUserProjectTeam(userId),
                    Email = await GetUserName(userId.ToString()) ?? string.Empty,
                    Details = await _repositoryWrapper.UserDetailsRepository.GetUserDetails(userId) ?? new UserDetails(),
                    Qualifications = await _repositoryWrapper.QualificationRepository.GetQualifications(userId),
                    ContactInfos = await _repositoryWrapper.ContactInfoRepository.GetContactInfos(userId),
                    EmergencyContacts = await _repositoryWrapper.EmergencyContactRepository.GetEmergencyContacts(userId),
                    UserNameVMs = await _repositoryWrapper.UserRepository.GetAllUserNames()
                };

                return userProfile; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
