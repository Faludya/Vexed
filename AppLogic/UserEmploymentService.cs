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

        public UserEmployment GetUserEmploymentById(int id)
        {
            return _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentById(id);
        }

        public UserEmployment GetUserEmploymentByUserId(Guid userId)
        {
            return _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentByUserId(userId);
        }

        public List<UserEmployment> GetUsersEmployment(Guid userId)
        {
            return _repositoryWrapper.UserEmploymentRepository.GetUserEmployments(userId);
        }

        public void UpdateUserEmployment(UserEmployment userEmployment)
        {
            _repositoryWrapper.UserEmploymentRepository.Update(userEmployment);
            _repositoryWrapper.Save();
        }
    }
}
