using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface IUserDetailsRepository : IRepositoryBase<UserDetails>
    {
        UserDetails GetUserDetailsById(int id);
        List<UserDetails> GetUserDetails(Guid userId);
    }
}