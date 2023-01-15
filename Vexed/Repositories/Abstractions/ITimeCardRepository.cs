using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface ITimeCardRepository : IRepositoryBase<TimeCard>
    {
        TimeCard GetTimeCardById(int id);
        List<TimeCard> GetTimeCards(Guid userId);
    }
}
