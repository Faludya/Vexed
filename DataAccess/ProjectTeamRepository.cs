﻿using Abstractions.Repositories;
using DataModels;
using Microsoft.EntityFrameworkCore;
using Shared;
using Vexed.Repositories;

namespace DataAccess
{
    public class ProjectTeamRepository : RepositoryBase<ProjectTeam>, IProjectTeamRepository
    {
        private Logger _logger;
        public ProjectTeamRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public override async Task<IQueryable<ProjectTeam>> FindAll()
        {
            return await Task.Run(() => _vexedDbContext.Set<ProjectTeam>()
                .Include(pt => pt.Project) // Include the related Project
                .AsNoTracking());
        }


        public async Task<ProjectTeam> GetProjectTeamById(int id)
        {
            try
            {
                return await _vexedDbContext.ProjectTeams.Where(l => l.Id == id)
                    .Include(pt => pt.Project)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}