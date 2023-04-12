using Microsoft.EntityFrameworkCore;
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

        public async Task<TimeCard> GetLastTimeCard(Guid userId)
        {
            return await _vexedDbContext.TimeCards.Where(u => u.UserId == userId).OrderByDescending(u => u.EndDate).FirstOrDefaultAsync();
        }

        public async Task<TimeCard> GetTimeCardById(int id)
        {
            return await _vexedDbContext.TimeCards.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TimeCard>> GetTimeCards(Guid userId)
        {
            return await _vexedDbContext.TimeCards.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<List<TimeCard>> GetTimeCardsSuperior(Guid superiorId)
        {
            return await _vexedDbContext.TimeCards.Where(t => t.SuperiorId == superiorId).ToListAsync();
        }
    }
}
