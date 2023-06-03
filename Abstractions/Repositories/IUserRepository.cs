using DataModels.ViewModels;
using Microsoft.AspNetCore.Identity;


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

        /// <summary>
        /// Returns all the Roles a user can have
        /// </summary>
        Task<List<string>> GetAllUserRoles();

        /// <summary>
        /// Returns the Roles the user with the UserId has
        /// </summary>
        Task<List<string>> GetUserRoles(Guid userId);

        Task CreateUserRole(Guid userId, string roleName);
        Task UpdateUserRoles(Guid userId, List<string> selectedRoles);
        Task DeleteUserRoles(Guid userId);
    }
}
