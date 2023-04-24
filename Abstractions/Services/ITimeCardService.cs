using Shared.ViewModels;
using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface ITimeCardService
    {
        /// <summary>
        /// Creates a new Time Card.
        /// </summary>
        Task CreateTimeCard(TimeCard timeCard);

        /// <summary>
        /// Updates the details of a Time Card
        /// </summary>
        Task UpdateTimeCard(TimeCard timeCard);

        /// <summary>
        /// Removes the Time Card from the database.
        /// </summary>
        Task DeleteTimeCard(TimeCard timeCard);

        /// <summary>
        /// Returns the first Time Card with the given <c>id</c>
        /// </summary>
        Task<TimeCard> GetTimeCardById(int id);

        /// <summary>
        /// Returns the last created Time Card for the given <c>userId</c>.
        /// Start date is set to the first date of the current week and the End date is set to the Friday of the current week.
        /// </summary>
        Task<TimeCard> GetLastTimeCard(Guid userId);

        /// <summary>
        /// Returns all the Time Cards from the database.
        /// </summary>
        Task<List<UserTimeCardsViewModel>> GetTimeCardsHR();

        /// <summary>
        /// Returns all the Time Cards for a given user.
        /// </summary>
        Task<List<TimeCard>> GetTimeCards(Guid userId);

        /// <summary>
        /// Returns all the Time Cards that belong to the superior.
        /// </summary>
        Task<List<TimeCard>> GetTimeCardsForSuperior(Guid superiorId);

        /// <summary>
        /// Method <c>GetLocationTypes</c> returns a list of all Types of Locations.
        /// </summary>
        List<string> GetLocationTypes();

        /// <summary>
        /// Method <c>GetLocationTypes</c> returns a list of all Types of Locations and moves the selected type to the first position.
        /// </summary>
        List<string> GetLocationTypes(string selectedLocation);
    }
}
