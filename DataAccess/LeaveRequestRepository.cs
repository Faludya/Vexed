using Microsoft.EntityFrameworkCore;
using Shared;
using DataAccess;
using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Shared.ViewModels;

namespace Vexed.Repositories
{
    public class LeaveRequestRepository : RepositoryBase<LeaveRequest>, ILeaveRequestRepository
    {
        private Logger _logger;
        public LeaveRequestRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<LeaveRequest> GetLeaveRequestById(int id)
        {
            try
            {
                return await _vexedDbContext.LeaveRequests.Where(l => l.Id == id).FirstOrDefaultAsync();
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
                return await _vexedDbContext.LeaveRequests.Where(l => l.UserId == userId).ToListAsync();
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
                var approvedBySuperiorRequests = await _vexedDbContext.LeaveRequests
                    .Where(lr => lr.Status == StatusManager.SuperiorApproval)
                    .OrderByDescending(lr => lr.EndDate)
                    .Join(_vexedDbContext.UsersDetails,
                                    lr => lr.UserId,
                                    ud => ud.UserId,
                                    (lr, ud) => new UserLeaveRequestsViewModel
                                    {
                                        LeaveRequest = lr,
                                        UserDetails = ud
                                    })
                    .ToListAsync();

                var otherRequests = await _vexedDbContext.LeaveRequests
                    .Where(lr => lr.Status != StatusManager.SuperiorApproval)
                    .OrderByDescending(lr => lr.EndDate)
                    .Join(_vexedDbContext.UsersDetails,
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

        public async Task<List<LeaveRequest>> GetLeaveRequestsSuperior(Guid superiorId)
        {
            try
            {
                return await _vexedDbContext.LeaveRequests.Where(l => l.SuperiorId == superiorId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
