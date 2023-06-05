using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DataAccess;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected VexedDbContext _vexedDbContext { get; set; }

        public RepositoryBase(VexedDbContext vexedDbContext)
        {
            _vexedDbContext = vexedDbContext;
        }

        public virtual async Task<IQueryable<T>> FindAll()
        {
            return await Task.Run(() => _vexedDbContext.Set<T>().AsNoTracking());
        }

        public async Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => _vexedDbContext.Set<T>().Where(expression).AsNoTracking());
        }

        public async Task Create(T entity)
        {
            await _vexedDbContext.Set<T>().AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            await Task.Run(() => _vexedDbContext.Set<T>().Update(entity));
        }

        public async Task Delete(T entity)
        {
            _vexedDbContext.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }
    }
}
