using Vexed.Repositories;
using Abstractions.Repositories;
using Shared;
using DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ToDoRepository : RepositoryBase<ToDo>, IToDoRepository
    {
        private readonly Logger _logger;
        public ToDoRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<ToDo?> GetToDoById(int id)
        {
            try
            {
                return await _vexedDbContext.ToDos.Where(t => t.Id == id).FirstOrDefaultAsync();
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
                return await _vexedDbContext.ToDos.Where(t => t.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public void UpdateTaskStatus(int id, bool isCompleted)
        {
            try
            {
                var task = _vexedDbContext.ToDos.Where(t => t.Id == id).FirstOrDefault();

                if (task != null)
                {
                    task.Completed = isCompleted;
                    _vexedDbContext.ToDos.Update(task);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
