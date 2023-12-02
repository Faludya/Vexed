using Microsoft.EntityFrameworkCore;
using Shared;
using DataModels;
using Vexed.Repositories.Abstractions;
using DataAccess;
using DataModels.ViewModels;

namespace Vexed.Repositories
{
    public class TimeCardRepository : RepositoryBase<TimeCard>, ITimeCardRepository
    {
        private readonly Logger _logger;
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

        public async Task<TimeCard?> GetLastTimeCard(Guid userId)
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

        public async Task<TimeCard?> GetTimeCardById(int id)
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

        public async Task<List<UserTimeCardsViewModel>> GetTimeCardsSuperior(Guid superiorId)
        {
            try
            {
                var submittedCards = await _vexedDbContext.TimeCards
                    .Where(c => c.Status == StatusManager.Submitted)
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
                    .Where(c => c.Status != StatusManager.Submitted)
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

                var userTimeCards = submittedCards.Concat(otherCards).ToList();

                return userTimeCards;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public float GetTotalWorkedHours(Guid userId)
        {
            try
            {
                var currentDate = DateTime.Now;
                var currentMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0, DateTimeKind.Local);
                var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                var approvedTimecards = _vexedDbContext.TimeCards
                    .Where(tc => tc.UserId == userId &&
                                  tc.Status == StatusManager.HRApproval &&
                                  tc.StartDate >= currentMonthStart && tc.EndDate <= currentMonthEnd)
                    .ToList();

                float totalWorkedHours = 0;
                totalWorkedHours += approvedTimecards.Sum(tc => tc.TotalHours);

                return totalWorkedHours;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }


        public int GetTotalWorkedDays(Guid userId)
        {
            try
            {
                var currentDate = DateTime.Now;
                var currentMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0, DateTimeKind.Local);
                var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

                var approvedTimecards = _vexedDbContext.TimeCards
                    .Where(tc => tc.UserId == userId && tc.Status == StatusManager.HRApproval &&
                                 tc.StartDate >= currentMonthStart && tc.EndDate <= currentMonthEnd)
                    .ToList();

                int totalWorkedDays = 0;
                foreach (var timeCard in approvedTimecards)
                {
                    TimeSpan duration = timeCard.EndDate - timeCard.StartDate;
                    int workedDays = duration.Days + 1;
                    totalWorkedDays += workedDays;
                }

                return totalWorkedDays;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
