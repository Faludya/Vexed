using Vexed.Repositories;
using Abstractions.Repositories;
using Shared;
using DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class QualificationRepository : RepositoryBase<Qualification>, IQualificationRepository
    {
        private Logger _logger;
        public QualificationRepository(VexedDbContext vexedDbContext, Logger logger) : base(vexedDbContext)
        {
            _logger = logger;
        }

        public async Task<Qualification> GetQualificationById(int id)
        {
            try
            {
                return await _vexedDbContext.Qualifications.Where(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<Qualification>> GetQualifications(Guid userId)
        {
            try
            {
                return await _vexedDbContext.Qualifications.Where(c => c.UserId == userId)
                    .OrderByDescending(q => q.DateObtained)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
