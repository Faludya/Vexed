using Microsoft.AspNetCore.Identity;
using Shared.ViewModels;
using Shared.ViewModels.CombinedClasses;

namespace Vexed.Services.Abstractions
{
    public interface IUserService
    {
        Task<string> GetUserName(string userId);
        Task<List<IdentityUser>> GetAllUsers();
        Task<List<UserNameVM>> GetUnassignedUserDetails();
        Task<List<UserNameVM>> GetUnassignedUserEmployment();
        Task<List<UserNameVM>> GetAllUserNames();
        OrganizationChartViewModel GetOrganizationChart(Guid userId);

        Task<List<string>> GetAllUserRoles();
        Task<List<string>> GetUserRoles(Guid userId);
        Task CreateUserRole(Guid userId,  string roleName);
        Task UpdateUserRoles(Guid userId, List<string> selectedRoles);

        Task<List<CardsVM>> GetLastCards(Guid userId);
    }
}
