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

        public void CreateUserDetails(UserDetails userDetails)
        {
            _repositoryWrapper.UserDetailsRepository.Create(userDetails);
            _repositoryWrapper.Save();
        }

        public void DeleteUserDetails(UserDetails userDetails)
        {
            _repositoryWrapper.UserDetailsRepository.Delete(userDetails);
            _repositoryWrapper.Save();
        }

        public List<UserDetails> GetAllUsersDetails()
        {
            return _repositoryWrapper.UserDetailsRepository.FindAll().ToList();
        }

        public void UpdateUserDetails(UserDetails userDetails)
        {
            _repositoryWrapper.UserDetailsRepository.Update(userDetails);
            _repositoryWrapper.Save();
        }
    }
}
