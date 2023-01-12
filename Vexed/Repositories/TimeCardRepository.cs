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
    }
}
