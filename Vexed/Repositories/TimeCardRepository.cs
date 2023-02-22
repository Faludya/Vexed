using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class TimeCardRepository : RepositoryBase<TimeCard>, ITimeCardRepository
    {
        public TimeCardRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }

        public TimeCard GetTimeCardById(int id)
        {
            return _vexedDbContext.TimeCards.Where(t => t.Id == id).First();
        }

        public List<TimeCard> GetTimeCards(Guid userId)
        {
            return _vexedDbContext.TimeCards.Where(t => t.UserId == userId).ToList();
        }

        public List<TimeCard> GetTimeCardsSuperior(Guid superiorId)
        {
            return _vexedDbContext.TimeCards.Where(t => t.SuperiorId == superiorId).ToList();
        }
    }
}
