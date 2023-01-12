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
    }
}
