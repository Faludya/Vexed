﻿using DataModels;
using Vexed.Repositories.Abstractions;

namespace Abstractions.Repositories
{
    public interface IProjectTeamRepository : IRepositoryBase<ProjectTeam>
    {
        /// <summary>
        /// Returns the first Project Team with the given <c>id</c>
        /// </summary>
        Task<ProjectTeam> GetProjectTeamById(int id);
    }
}
