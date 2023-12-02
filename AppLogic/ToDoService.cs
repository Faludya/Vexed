using Abstractions.Services;
using DataModels;
using Shared;
using Vexed.Repositories.Abstractions;

namespace AppLogic
{
    public class ToDoService : IToDoService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly Logger _logger;

        public ToDoService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateToDo(ToDo task)
        {
            try
            {
                await _repositoryWrapper.ToDoRepository.Create(task);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteToDo(ToDo task)
        {
            try
            {
                await _repositoryWrapper.ToDoRepository.Delete(task);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<ToDo?> GetToDoById(int id)
        {
            try
            {
                return await _repositoryWrapper.ToDoRepository.GetToDoById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<ToDo>> GetToDoList(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.ToDoRepository.GetToDoList(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateToDo(ToDo task)
        {
            try
            {
                await _repositoryWrapper.ToDoRepository.Update(task);
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
