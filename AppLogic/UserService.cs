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

        public List<UserNameVM> GetAllUserNames()
        {
            return _repositoryWrapper.UserRepository.GetAllUserNames();
        }

        public List<IdentityUser> GetAllUsers()
        {
            return _repositoryWrapper.UserRepository.GetAllUsers();
        }

        public List<UserNameVM> GetUnassignedUserDetails()
        {
            var allUsersIds = _repositoryWrapper.UserRepository.GetAllUserIds();
            var usedIds = _repositoryWrapper.UserDetailsRepository.GetAllUserDetailIds();
            var unusedUserIds = allUsersIds.Except(usedIds).ToList();

            return _repositoryWrapper.UserRepository.GetUnsusedUserNames(unusedUserIds);
        }

        public List<UserNameVM> GetUnassignedUserEmployment()
        {
            var allUsersIds = _repositoryWrapper.UserRepository.GetAllUserIds();
            var usedIds = _repositoryWrapper.UserEmploymentRepository.GetAllUserEmploymentIds();
            var unusedUserIds = allUsersIds.Except(usedIds).ToList();

            return _repositoryWrapper.UserRepository.GetUnsusedUserNames(unusedUserIds);
        }

        public string GetUserName(string userId)
        {
            return _repositoryWrapper.UserRepository.GetUserName(userId);
        }
    }
}
