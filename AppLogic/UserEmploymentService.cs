using Microsoft.EntityFrameworkCore;
using Shared;
using DataModels;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class UserEmploymentService : IUserEmploymentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly Logger _logger;

        public UserEmploymentService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateUserEmployment(UserEmployment userEmployment)
        {
            try
            {
                await _repositoryWrapper.UserEmploymentRepository.Create(userEmployment);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteUserEmployment(UserEmployment userEmployment)
        {
            try
            {
                await _repositoryWrapper.UserEmploymentRepository.Delete(userEmployment);
                await _repositoryWrapper.UserRepository.DeleteUserRoles(userEmployment.UserId);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserEmployment>> GetAllUsersEmployment()
        {
            try
            {
                var queryable = await _repositoryWrapper.UserEmploymentRepository.FindAll();
                return await queryable.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserEmployment> GetUserEmploymentById(int id)
        {
            try
            {
                return await _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserEmployment> GetUserEmploymentByUserId(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentByUserId(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserEmployment>> GetUsersEmployment(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.UserEmploymentRepository.GetUserEmployments(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateUserEmployment(UserEmployment userEmployment)
        {
            try
            {
                await _repositoryWrapper.UserEmploymentRepository.Update(userEmployment);
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
