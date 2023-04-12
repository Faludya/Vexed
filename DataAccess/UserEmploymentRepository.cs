using Microsoft.EntityFrameworkCore;
using Vexed.Data;
using Vexed.Models;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class UserEmploymentRepository : RepositoryBase<UserEmployment>, IUserEmploymentRepository
    {
        public UserEmploymentRepository(VexedDbContext vexedDbContext) : base(vexedDbContext)
        {
        }

        public async Task<List<string>> GetAllUserEmploymentIds()
        {
            return await _vexedDbContext.UsersEmployments.Select(u => u.UserId.ToString().ToLower()).ToListAsync();
        }

        public async Task<UserEmployment> GetUserEmploymentById(int id)
        {
            return await _vexedDbContext.UsersEmployments.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserEmployment> GetUserEmploymentByUserId(Guid userId)
        {
            return await _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<List<UserEmployment>> GetUserEmployments(Guid userId)
        {
            return await _vexedDbContext.UsersEmployments.Where(u => u.UserId == userId).ToListAsync();
        }
    }
}
