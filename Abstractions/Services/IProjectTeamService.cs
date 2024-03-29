﻿using DataModels;

namespace Abstractions.Services
{
    public interface IProjectTeamService
    {
        /// <summary>
        /// Creates a new Project Team.
        /// </summary>
        Task CreateProjectTeam(ProjectTeam projectTeam);

        /// <summary>
        /// Updates the details of a Project Team.
        /// </summary>
        Task UpdateProjectTeam(ProjectTeam projectTeam);

        /// <summary>
        /// Removes the Project Team from the database.
        /// </summary>
        Task DeleteProjectTeam(ProjectTeam projectTeam);

        /// <summary>
        /// Returns the first Project Team with the given <c>id</c>
        /// </summary>
        Task<ProjectTeam> GetProjectTeamById(int id);

        /// <summary>
        /// Returns all the Project Teams from the database.
        /// </summary>
        Task<List<ProjectTeam>> GetAllProjectTeams();

        /// <summary>
        /// Returns the current Project Team with the given <c>projectId</c> and userid
        /// </summary>
        Task<List<ProjectTeam>> GetUserProjectTeam(Guid userId);

        /// <summary>
        /// Returns all the members for the Project.
        /// </summary>
        Task<List<ProjectTeam>> GetProjectTeamMembers(int projectId);
    }
}
