using DataModels;

namespace Abstractions.Services
{
    public interface IToDoService
    {
        /// <summary>
        /// Creates a new ToDo task
        /// </summary>
        Task CreateToDo(ToDo task);

        /// <summary>
        /// Updates the ToDo task
        /// </summary>
        Task UpdateToDo(ToDo task);

        /// <summary>
        /// Removes the ToDo task from the database.
        /// </summary>
        Task DeleteToDo(ToDo task);

        /// <summary>
        /// Returns the first ToDo task with the given <c>id</c>
        /// </summary>
        Task<ToDo> GetToDoById(int id);

        /// <summary>
        /// Returns all the tasks of a user
        /// </summary>
        Task<List<ToDo>> GetToDoList(Guid userId);
    }
}
