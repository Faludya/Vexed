using System.Linq.Expressions;

namespace Vexed.Repositories.Abstractions
{
    public interface IRepositoryBase<T>
    {
        Task<IQueryable<T>> FindAll();
        Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
