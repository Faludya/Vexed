using Microsoft.AspNetCore.Identity;
using Shared;
using System.Linq;
using Vexed.Models.ViewModels;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

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
    }
}
