using Shared;
using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private Logger _logger;

        public UserDetailsService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateUserDetails(UserDetails userDetails)
        {
            try
            {
                await _repositoryWrapper.UserDetailsRepository.Create(userDetails);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteUserDetails(UserDetails userDetails)
        {
            try
            {
                await _repositoryWrapper.UserDetailsRepository.Delete(userDetails);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserDetails>> GetAllUsersDetails()
        {
            try
            {
                return await _repositoryWrapper.UserDetailsRepository.GetAllUserDetails();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserDetails> GetUserDetailsById(int id)
        {
            try
            {
                return await _repositoryWrapper.UserDetailsRepository.GetUserDetailsById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserDetails> GetUserDetailsByUserId(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.UserDetailsRepository.GetUserDetailsByUserId(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserDetails>> GetUsersDetails(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.UserDetailsRepository.GetUserDetails(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateUserDetails(UserDetails userDetails)
        {
            try
            {
                await _repositoryWrapper.UserDetailsRepository.Update(userDetails);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
