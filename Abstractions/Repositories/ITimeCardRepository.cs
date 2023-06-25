using DataModels;
using DataModels.ViewModels;

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
        Task<List<UserTimeCardsViewModel>> GetTimeCardsSuperior(Guid superiorId);

        /// <summary>
        /// Returns all the Time Cards sorted by date and status
        /// </summary>
        Task<List<UserTimeCardsViewModel>> GetTimeCardsHR();

        /// <summary>
        /// Returns all the worked hours of a user for the current month.
        /// </summary>
        Task<float> GetTotalWorkedHours(Guid superiorId);

        /// <summary>
        /// Returns all the worked days of a user for the current month.
        /// </summary>
        Task<int> GetTotalWorkedDays(Guid superiorId);
    }
}
