using Abstractions.Services;
using DataModels;
using Shared;
using Vexed.Repositories.Abstractions;

namespace AppLogic
{
    public class ProjectTeamService : IProjectTeamService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private Logger _logger;

        public ProjectTeamService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateProjectTeam(ProjectTeam projectTeam)
        {
            try
            {
                await _repositoryWrapper.ProjectTeamRepository.Create(projectTeam);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteProjectTeam(ProjectTeam projectTeam)
        {
            try
            {
                await _repositoryWrapper.ProjectTeamRepository.Delete(projectTeam);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<ProjectTeam>> GetAllProjectTeams()
        {
            try
            {
                var queariable = await _repositoryWrapper.ProjectTeamRepository.FindAll();
                return queariable.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<ProjectTeam> GetProjectTeamById(int id)
        {
            try
            {
                return await _repositoryWrapper.ProjectTeamRepository.GetProjectTeamById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateProjectTeam(ProjectTeam projectTeam)
        {
            try
            {
                await _repositoryWrapper.ProjectTeamRepository.Update(projectTeam);
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
