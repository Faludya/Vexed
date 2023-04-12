using Microsoft.AspNetCore.Identity;
using System.Linq;
using Vexed.Models.ViewModels;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<UserNameVM>> GetAllUserNames()
        {
            return await _repositoryWrapper.UserRepository.GetAllUserNames();
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            return await _repositoryWrapper.UserRepository.GetAllUsers();
        }

        public async Task<List<UserNameVM>> GetUnassignedUserDetails()
        {
            var allUsersIds = await _repositoryWrapper.UserRepository.GetAllUserIds();
            var usedIds = await _repositoryWrapper.UserDetailsRepository.GetAllUserDetailIds();
            var unusedUserIds = allUsersIds.Except(usedIds).ToList();

            return await _repositoryWrapper.UserRepository.GetUnsusedUserNames(unusedUserIds);
        }

        public async Task<List<UserNameVM>> GetUnassignedUserEmployment()
        {
            var allUsersIds = await _repositoryWrapper.UserRepository.GetAllUserIds();
            var usedIds = await _repositoryWrapper.UserEmploymentRepository.GetAllUserEmploymentIds();
            var unusedUserIds = allUsersIds.Except(usedIds).ToList();

            return await _repositoryWrapper.UserRepository.GetUnsusedUserNames(unusedUserIds);
        }

        public async Task<string> GetUserName(string userId)
        {
            return await _repositoryWrapper.UserRepository.GetUserName(userId);
        }
    }
}
