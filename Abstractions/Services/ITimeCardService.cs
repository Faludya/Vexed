using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface ITimeCardService
    {
        /// <summary>
        /// Creates a new Time Card.
        /// </summary>
        void CreateTimeCard(TimeCard timeCard);

        /// <summary>
        /// Updates the details of a Time Card
        /// </summary>
        void UpdateTimeCard(TimeCard timeCard);

        /// <summary>
        /// Removes the Time Card from the database.
        /// </summary>
        void DeleteTimeCard(TimeCard timeCard);

        /// <summary>
        /// Returns the first Time Card with the given <c>id</c>
        /// </summary>
        TimeCard GetTimeCardById(int id);

        /// <summary>
        /// Returns the last created Time Card for the given <c>userId</c>.
        /// Start date is set to the first date of the current week and the End date is set to the Friday of the current week.
        /// </summary>
        TimeCard GetLastTimeCard(Guid userId);

        /// <summary>
        /// Returns all the Time Cards from the database.
        /// </summary>
        List<TimeCard> GetAllTimeCards();

        /// <summary>
        /// Returns all the Time Cards for a given user.
        /// </summary>
        List<TimeCard> GetTimeCards(Guid userId);

        /// <summary>
        /// Returns all the Time Cards that belong to the superior.
        /// </summary>
        List<TimeCard> GetTimeCardsForSuperior(Guid superiorId);

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
