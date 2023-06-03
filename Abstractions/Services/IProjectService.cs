using DataModels;

namespace Abstractions.Services
{
    public interface IProjectService
    {
        /// <summary>
        /// Creates a new Project.
        /// </summary>
        Task CreateProject(Project project);

        /// <summary>
        /// Updates the details of a Project.
        /// </summary>
        Task UpdateProject(Project project);

        /// <summary>
        /// Removes the Project from the database.
        /// </summary>
        Task DeleteProject(Project project);

        /// <summary>
        /// Returns the first Project with the given <c>id</c>
        /// </summary>
        Task<Project> GetProjectById(int id);

        /// <summary>
        /// Returns all the Projects from the database.
        /// </summary>
        Task<List<Project>> GetAllProjects();
    }
}
