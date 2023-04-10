using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vexed.Data;
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

        public IQueryable<T> FindAll()
        {
            return _vexedDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _vexedDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            _vexedDbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _vexedDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _vexedDbContext.Set<T>().Remove(entity);
        }
    }
}
