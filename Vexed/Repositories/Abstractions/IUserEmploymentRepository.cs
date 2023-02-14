using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface IUserEmploymentRepository : IRepositoryBase<UserEmployment>
    {
        /// <summary>
        /// Returns the first User Employment with the given <c>id</c>
        /// </summary>
        UserEmployment GetUserEmploymentById(int id);

        /// <summary>
        /// Returns all the User Employment for a given user.
        /// </summary>
        List<UserEmployment> GetUserEmployments(Guid userId);
    }
}