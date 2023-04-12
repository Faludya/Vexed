﻿using Microsoft.AspNetCore.Identity;
using Vexed.Models;
using Vexed.Models.ViewModels;

namespace Vexed.Repositories.Abstractions
{
    public interface IUserRepository : IRepositoryBase<IdentityUser>
    {
        /// <summary>
        /// Returns the UserName of the user
        /// </summary>
        Task<string> GetUserName(string userId);
        /// <summary>
        /// Returns the Guid of the superior for the given user.
        /// </summary>
        Task<Guid> GetUserSuperior(Guid userId);

        /// <summary>
        /// Returns all the identity users
        /// </summary>
        Task<List<IdentityUser>> GetAllUsers();

        /// <summary>
        /// Returns all the  UserIds
        /// </summary>
        Task<List<string>> GetAllUserIds();

        /// <summary>
        /// Returns the UserName and UserId of all the users that are not added
        /// </summary>
        Task<List<UserNameVM>> GetUnsusedUserNames(List<string> userIds);

        /// <summary>
        /// Returns all UserNames and UserIds
        /// </summary>
        Task<List<UserNameVM>> GetAllUserNames();

    }
}
