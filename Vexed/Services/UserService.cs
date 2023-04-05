using Microsoft.AspNetCore.Identity;
using Vexed.Models.ViewModels;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public List<IdentityUser> GetAllUsers()
        {
            return _repositoryWrapper.UserRepository.GetAllUsers();
        }

        public List<UserNameVM> GetUnassignedUserNames()
        {
            return _repositoryWrapper.UserRepository.GetUnassignedUserNames();
        }
    }
}
