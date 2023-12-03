using Microsoft.EntityFrameworkCore;
using Shared;
using DataAccess;
using DataModels;
using Vexed.Repositories.Abstractions;
using DataModels.ViewModels;
using Vexed.Models;

namespace Vexed.Repositories
{
    public class LeaveRequestRepository : RepositoryBase<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly Logger _logger;
        public LeaveRequestRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public float GetLeaveHours(Guid userId)
        {
            try
            {
                var currentMonthRange = GetCurrentMonthRange();

                var approvedLeaves = _vexedDbContext.LeaveRequests?
                    .Where(lr => lr.UserId == userId && lr.Status == StatusManager.HRApproval &&
                                 lr.StartDate >= currentMonthRange.Start && lr.EndDate <= currentMonthRange.End)
                    .ToList() ?? new List<LeaveRequest>();

                float totalPaidHours = approvedLeaves.Sum(lr => lr.TotalHours) ?? 0;

                return totalPaidHours;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        private static (DateTime Start, DateTime End) GetCurrentMonthRange()
        {
            var currentDate = DateTime.Now;
            var currentMonthStart = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0, DateTimeKind.Local);
            var currentMonthEnd = currentMonthStart.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

            return (currentMonthStart, currentMonthEnd);
        }

        public int GetLeaveDays(Guid userId)
        {
            try
            {
                var currentMonthRange = GetCurrentMonthRange();

                var approvedLeaves = _vexedDbContext.LeaveRequests!
                    .Where(lr => lr.UserId == userId && lr.Status == StatusManager.HRApproval &&
                                 lr.StartDate >= currentMonthRange.Start && lr.EndDate <= currentMonthRange.End)
                    .ToList();

                int totalDays = 0;
                foreach (var timeCard in approvedLeaves)
                {
                    TimeSpan duration = timeCard.EndDate - timeCard.StartDate;
                    int workedDays = duration.Days + 1;
                    totalDays += workedDays;
                }

                return totalDays;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<LeaveRequest> GetLeaveRequestById(int id)
        {
            try
            {
                return await _vexedDbContext.LeaveRequests!.Where(l => l.Id == id).FirstAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<LeaveRequest>> GetLeaveRequests(Guid userId)
        {
            try
            {
                return await _vexedDbContext.LeaveRequests!.Where(l => l.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<UserLeaveRequestsViewModel>> GetLeaveRequestsHR()
        {
            try
            {
                var approvedBySuperiorRequests = await _vexedDbContext.LeaveRequests!
                    .Where(lr => lr.Status == StatusManager.SuperiorApproval)
                    .OrderByDescending(lr => lr.EndDate)
                    .Join(_vexedDbContext.UsersDetails!,
                                    lr => lr.UserId,
                                    ud => ud.UserId,
                                    (lr, ud) => new UserLeaveRequestsViewModel
                                    {
                                        LeaveRequest = lr,
                                        UserDetails = ud
                                    })
                    .ToListAsync();

                var otherRequests = await _vexedDbContext.LeaveRequests!
                    .Where(lr => lr.Status != StatusManager.SuperiorApproval)
                    .OrderByDescending(lr => lr.EndDate)
                    .Join(_vexedDbContext.UsersDetails!,
                                    lr => lr.UserId,
                                    ud => ud.UserId,
                                    (lr, ud) => new UserLeaveRequestsViewModel
                                    {
                                        LeaveRequest = lr,
                                        UserDetails = ud
                                    })
                    .ToListAsync();

                var userLeaveRequests = approvedBySuperiorRequests.Concat(otherRequests).ToList();

                return userLeaveRequests;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        }

        public async Task<List<UserLeaveRequestsViewModel>> GetLeaveRequestsSuperior(Guid superiorId)
        {
            try
            {
                var submittedLeaves = await _vexedDbContext.LeaveRequests!
                    .Where(lr => lr.Status == StatusManager.Submitted)
                    .OrderByDescending(lr => lr.EndDate)
                    .Join(_vexedDbContext.UsersDetails!,
                                    lr => lr.UserId,
                                    ud => ud.UserId,
                                    (lr, ud) => new UserLeaveRequestsViewModel
                                    {
                                        LeaveRequest = lr,
                                        UserDetails = ud
                                    })
                    .ToListAsync();

                var otherRequests = await _vexedDbContext.LeaveRequests!
                    .Where(lr => lr.Status != StatusManager.Submitted)
                    .OrderByDescending(lr => lr.EndDate)
                    .Join(_vexedDbContext.UsersDetails!,
                                    lr => lr.UserId,
                                    ud => ud.UserId,
                                    (lr, ud) => new UserLeaveRequestsViewModel
                                    {
                                        LeaveRequest = lr,
                                        UserDetails = ud
                                    })
                    .ToListAsync();

                var userLeaveRequests = submittedLeaves.Concat(otherRequests).ToList();

                return userLeaveRequests;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
