using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IUserDetailsService
    {
        void CreateUserDetails(UserDetails userDetails);
        void UpdateUserDetails(UserDetails userDetails);
        void DeleteUserDetails(UserDetails userDetails);
        List<UserDetails> GetAllUsersDetails();
    }
}
