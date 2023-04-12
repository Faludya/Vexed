using Microsoft.AspNetCore.Identity;
using Vexed.Models.ViewModels;

namespace Vexed.Services.Abstractions
{
    public interface IUserService
    {
        Task<string> GetUserName(string userId);
        Task<List<IdentityUser>> GetAllUsers();
        Task<List<UserNameVM>> GetUnassignedUserDetails();
        Task<List<UserNameVM>> GetUnassignedUserEmployment();
        Task<List<UserNameVM>> GetAllUserNames();
    }
}
