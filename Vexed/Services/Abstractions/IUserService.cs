using Microsoft.AspNetCore.Identity;
using Vexed.Models.ViewModels;

namespace Vexed.Services.Abstractions
{
    public interface IUserService
    {
        List<IdentityUser> GetAllUsers();
        List<UserNameVM> GetUnassignedUserNames();
    }
}
