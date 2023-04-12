using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface ITimeCardRepository : IRepositoryBase<TimeCard>
    {
        /// <summary>
        /// Returns the first Time Card with the given <c>id</c>
        /// </summary>
        Task<TimeCard> GetTimeCardById(int id);

        /// <summary>
        /// Returns the last Time Card for the given <c>id</c>
        /// </summary>
        Task<TimeCard> GetLastTimeCard(Guid userId);

        /// <summary>
        /// Returns all the Time Cards for a given user.
        /// </summary>
        Task<List<TimeCard>> GetTimeCards(Guid userId);

        /// <summary>
        /// Returns all the Time Cards for a given superior.
        /// </summary>
        Task<List<TimeCard>> GetTimeCardsSuperior(Guid superiorId);
    }
}
