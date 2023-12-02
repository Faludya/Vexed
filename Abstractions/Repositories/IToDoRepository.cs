using DataModels;
using Vexed.Repositories.Abstractions;

namespace Abstractions.Repositories
{
    public interface IToDoRepository : IRepositoryBase<ToDo>
    {
        /// <summary>
        /// Returns the task with the given id
        /// </summary>
        Task<ToDo> GetToDoById(int id);

        /// <summary>
        /// Returns the list of all the ToDo taks of the user
        /// </summary>
        Task<List<ToDo>> GetToDoList(Guid userId);

        /// <summary>
        /// Updates the task to be either completed or not
        /// </summary>
        void UpdateTaskStatus(int id, bool isCompleted);
    }
}
