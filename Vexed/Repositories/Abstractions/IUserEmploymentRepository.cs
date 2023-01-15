using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface IUserEmploymentRepository : IRepositoryBase<UserEmployment>
    {
        UserEmployment GetUserEmploymentById(int id);
        List<UserEmployment> GetUserEmployments(Guid userId);
    }
}