using Microsoft.AspNetCore.Identity;
using Vexed.Models.ViewModels;

namespace Vexed.Services.Abstractions
{
    public interface IUserService
    {
        string GetUserName(string userId);
        List<IdentityUser> GetAllUsers();
        List<UserNameVM> GetUnassignedUserDetails();
        List<UserNameVM> GetUnassignedUserEmployment();
        List<UserNameVM> GetAllUserNames();
    }
}
