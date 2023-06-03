using Abstractions.Services;
using DataModels;
using Shared;
using Vexed.Repositories.Abstractions;

namespace AppLogic
{
    public class ProjectService : IProjectService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private Logger _logger;

        public ProjectService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateProject(Project project)
        {
            try
            {
                await _repositoryWrapper.ProjectRepository.Create(project);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteProject(Project project)
        {
            try
            {
                await _repositoryWrapper.ProjectRepository.Delete(project);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<Project>> GetAllProjects()
        {
            try
            {
                var queriable =  await _repositoryWrapper.ProjectRepository.FindAll();
                return queriable.OrderByDescending(p => p.EndDate).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<Project> GetProjectById(int id)
        {
            try
            {
                return await _repositoryWrapper.ProjectRepository.GetProjectById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateProject(Project project)
        {
            try
            {
                await _repositoryWrapper.ProjectRepository.Update(project);
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
