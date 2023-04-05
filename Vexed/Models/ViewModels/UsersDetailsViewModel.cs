using Microsoft.AspNetCore.Identity;

namespace Vexed.Models.ViewModels
{
    public class UsersDetailsViewModel
    {
        public UserDetails UserDetails { get; set; }
        public List<UserNameVM> UserNamesVM { get; set; }
    }
}
