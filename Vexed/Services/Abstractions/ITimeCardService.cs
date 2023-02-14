using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface ITimeCardService
    {
        void CreateTimeCard(TimeCard timeCard);
        void UpdateTimeCard(TimeCard timeCard);
        void DeleteTimeCard(TimeCard timeCard);
        TimeCard GetTimeCardById(int id);
        List<TimeCard> GetAllTimeCards();
        List<TimeCard> GetTimeCards(Guid userId);

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
