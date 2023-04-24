using Microsoft.EntityFrameworkCore;
using Shared;
using DataAccess;
using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Shared.ViewModels;

namespace Vexed.Repositories
{
    public class TimeCardRepository : RepositoryBase<TimeCard>, ITimeCardRepository
    {
        private Logger _logger;
        public TimeCardRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<List<UserTimeCardsViewModel>> GetTimeCardsHR()
        {
            try
            {
                var approvedBySuperiorCards = await _vexedDbContext.TimeCards
                    .Where(c => c.Status == StatusManager.SuperiorApproval)
                    .OrderByDescending(c => c.EndDate)
                    .Join(_vexedDbContext.UsersDetails,
                                        tc => tc.UserId,
                                        ud => ud.UserId,
                                        (tc, ud) => new UserTimeCardsViewModel
                                        {
                                            TimeCard = tc,
                                            UserDetails = ud
                                        })
                    .ToListAsync();

                var otherCards = await _vexedDbContext.TimeCards
                    .Where(c => c.Status != StatusManager.SuperiorApproval)
                    .OrderByDescending(c => c.EndDate)
                    .Join(_vexedDbContext.UsersDetails,
                                        tc => tc.UserId,
                                        ud => ud.UserId,
                                        (tc, ud) => new UserTimeCardsViewModel
                                        {
                                            TimeCard = tc,
                                            UserDetails = ud
                                        })
                    .ToListAsync();

                var userTimeCards = approvedBySuperiorCards.Concat(otherCards).ToList();

                return userTimeCards;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<TimeCard> GetLastTimeCard(Guid userId)
        {
            try
            {
                return await _vexedDbContext.TimeCards.Where(u => u.UserId == userId).OrderByDescending(u => u.EndDate).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<TimeCard> GetTimeCardById(int id)
        {
            try
            {
                return await _vexedDbContext.TimeCards.Where(t => t.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<TimeCard>> GetTimeCards(Guid userId)
        {
            try
            {
                return await _vexedDbContext.TimeCards.Where(t => t.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<TimeCard>> GetTimeCardsSuperior(Guid superiorId)
        {
            try
            {
                return await _vexedDbContext.TimeCards.Where(t => t.SuperiorId == superiorId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
