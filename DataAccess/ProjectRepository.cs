using Abstractions.Repositories;
using DataModels;
using Microsoft.EntityFrameworkCore;
using Shared;
using Vexed.Repositories;

namespace DataAccess
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        private Logger _logger;
        public ProjectRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<Project> GetProjectById(int id)
        { 
            try
            {
                return await _vexedDbContext.Projects.Where(l => l.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
