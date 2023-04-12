using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserDetailsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task CreateUserDetails(UserDetails userDetails)
        {
            await _repositoryWrapper.UserDetailsRepository.Create(userDetails);
            await _repositoryWrapper.Save();
        }

        public async Task DeleteUserDetails(UserDetails userDetails)
        {
            await _repositoryWrapper.UserDetailsRepository.Delete(userDetails);
            await _repositoryWrapper.Save();
        }

        public async Task<List<UserDetails>> GetAllUsersDetails()
        {
            return await _repositoryWrapper.UserDetailsRepository.GetAllUserDetails();
        }

        public async Task<UserDetails> GetUserDetailsById(int id)
        {
            return await _repositoryWrapper.UserDetailsRepository.GetUserDetailsById(id);
        }

        public async Task<UserDetails> GetUserDetailsByUserId(Guid userId)
        {
            return await _repositoryWrapper.UserDetailsRepository.GetUserDetailsByUserId(userId);
        }

        public async Task<List<UserDetails>> GetUsersDetails(Guid userId)
        {
            return await _repositoryWrapper.UserDetailsRepository.GetUserDetails(userId);
        }

        public async Task UpdateUserDetails(UserDetails userDetails)
        {
            await _repositoryWrapper.UserDetailsRepository.Update(userDetails);
            await _repositoryWrapper.Save();
        }
    }
}
