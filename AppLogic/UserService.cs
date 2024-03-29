﻿using Microsoft.AspNetCore.Identity;
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
        private IRepositoryWrapper _repositoryWrapper;
        private Logger _logger;

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

        public async Task<string> GetUserName(string userId)
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
                OrganizationChartViewModel orgChar = new OrganizationChartViewModel();
                orgChar.TeamMembers = new List<UserInfoVM>();
                //Find the superior and add it first to the list
                var superiorEmployment = _repositoryWrapper.UserEmploymentRepository.GetUserSuperior(userId).Result;
                var superiorDetails = _repositoryWrapper.UserDetailsRepository.GetUserDetails(superiorEmployment.UserId).Result;
                var superiorEmail = _repositoryWrapper.UserRepository.GetUserName(superiorEmployment.UserId.ToString()).Result;

                var superiorProfile = new UserInfoVM()
                {
                    Employment = superiorEmployment,
                    Details = superiorDetails,
                    Email = superiorEmail
                };
                orgChar.Superior = superiorProfile;

                //Find the rest of the users and add them to the list
                List<UserEmployment> teamEmployments = _repositoryWrapper.UserEmploymentRepository.GetTeamMembersEmployment(superiorEmployment.UserId).Result;
                foreach(var member in teamEmployments)
                {
                    var userDetails = _repositoryWrapper.UserDetailsRepository.GetUserDetails(member.UserId).Result;
                    var userEmail = _repositoryWrapper.UserRepository.GetUserName(member.UserId.ToString()).Result;

                    var memberProfile = new UserInfoVM()
                    {
                        Employment = member,
                        Details = userDetails,
                        Email = userEmail
                    };

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
                var timeCards = await _repositoryWrapper.TimeCardRepository.GetTimeCards(userId);
                timeCards.Sort((t1, t2) => t2.StartDate.CompareTo(t2.StartDate));
                timeCards = timeCards.Take(10).ToList();

                var leaveRequests = await _repositoryWrapper.LeaveRequestRepository.GetLeaveRequests(userId);
                leaveRequests.Sort((l1, l2) => l2.StartDate.CompareTo(l1.StartDate));
                leaveRequests = leaveRequests.Take(10).ToList();

                // Combine the time cards and leave requests into a single list
                var combinedEntries = new List<CardsVM>();

                foreach (var timeCard in timeCards)
                {
                    combinedEntries.Add(new CardsVM { 
                        Name = timeCard.ProjectCode, 
                        Status = timeCard.Status, 
                        StartDate = timeCard.StartDate, 
                        EndDate = timeCard.EndDate
                    });
                }

                foreach (var leaveRequest in leaveRequests)
                {
                    combinedEntries.Add(new CardsVM { 
                        Name = leaveRequest.Type, 
                        Status = leaveRequest.Status, 
                        StartDate = leaveRequest.StartDate, 
                        EndDate = leaveRequest.EndDate
                    });
                }

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

        public async Task<UserProfileVM> GetUserProfile(Guid userId)
        {
            try
            {
                var userProfile = new UserProfileVM();

                userProfile.Employment = await _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentByUserId(userId);
                userProfile.ProjectTeams = await _repositoryWrapper.ProjectTeamRepository.GetUserProjectTeam(userId);
                userProfile.Email = await GetUserName(userId.ToString());
                userProfile.Details = await _repositoryWrapper.UserDetailsRepository.GetUserDetails(userId);
                userProfile.Qualifications = await _repositoryWrapper.QualificationRepository.GetQualifications(userId);
                userProfile.ContactInfos = await _repositoryWrapper.ContactInfoRepository.GetContactInfos(userId);
                userProfile.EmergencyContacts = await _repositoryWrapper.EmergencyContactRepository.GetEmergencyContacts(userId);
                userProfile.UserNameVMs = await _repositoryWrapper.UserRepository.GetAllUserNames();

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
