using DataModels;
using Vexed.Repositories.Abstractions;

namespace Abstractions.Repositories
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        /// <summary>
        /// Returns the first Project with the given <c>id</c>
        /// </summary>
        Task<Project> GetProjectById(int id);
    }
}
