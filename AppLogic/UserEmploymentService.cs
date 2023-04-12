using Microsoft.EntityFrameworkCore;
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

        public async Task CreateUserEmployment(UserEmployment userEmployment)
        {
            await _repositoryWrapper.UserEmploymentRepository.Create(userEmployment);
            await _repositoryWrapper.Save();
        }

        public async Task DeleteUserEmployment(UserEmployment userEmployment)
        {
            await _repositoryWrapper.UserEmploymentRepository.Delete(userEmployment);
            await _repositoryWrapper.Save();
        }

        public async Task<List<UserEmployment>> GetAllUsersEmployment()
        {
            var queryable = await _repositoryWrapper.UserEmploymentRepository.FindAll();
            return await queryable.ToListAsync();
        }

        public async Task<UserEmployment> GetUserEmploymentById(int id)
        {
            return await _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentById(id);
        }

        public async Task<UserEmployment> GetUserEmploymentByUserId(Guid userId)
        {
            return await _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentByUserId(userId);
        }

        public async Task<List<UserEmployment>> GetUsersEmployment(Guid userId)
        {
            return await _repositoryWrapper.UserEmploymentRepository.GetUserEmployments(userId);
        }

        public async Task UpdateUserEmployment(UserEmployment userEmployment)
        {
            await _repositoryWrapper.UserEmploymentRepository.Update(userEmployment);
            await _repositoryWrapper.Save();
        }
    }
}
