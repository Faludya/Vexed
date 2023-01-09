using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class UserEmploymentService : IUserEmploymentService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserEmploymentService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public void CreateUserEmployment(UserEmployment userEmployment)
        {
            _repositoryWrapper.UserEmploymentRepository.Create(userEmployment);
            _repositoryWrapper.Save();
        }

        public void DeleteUserEmployment(UserEmployment userEmployment)
        {
            _repositoryWrapper.UserEmploymentRepository.Delete(userEmployment);
            _repositoryWrapper.Save();
        }

        public List<UserEmployment> GetAllUsersEmployment()
        {
            return _repositoryWrapper.UserEmploymentRepository.FindAll().ToList();
        }

        public void UpdateUserEmployment(UserEmployment userEmployment)
        {
            _repositoryWrapper.UserEmploymentRepository.Update(userEmployment);
            _repositoryWrapper.Save();
        }
    }
}
