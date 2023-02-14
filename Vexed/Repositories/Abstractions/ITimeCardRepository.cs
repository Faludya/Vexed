using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface ITimeCardRepository : IRepositoryBase<TimeCard>
    {
        /// <summary>
        /// Returns the first Time Card with the given <c>id</c>
        /// </summary>
        TimeCard GetTimeCardById(int id);

        /// <summary>
        /// Returns all the Time Cards for a given user.
        /// </summary>
        List<TimeCard> GetTimeCards(Guid userId);
    }
}
