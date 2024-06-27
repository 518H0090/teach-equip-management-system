using System.Linq.Expressions;
using TeachEquipManagement.DAL.Specifications;

namespace TeachEquipManagement.DAL.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetBydIdAsync(Guid id);

        Task InsertAsync(TEntity entity);

        Task InsertRangeAsync(IEnumerable<TEntity> entity);

        void UpdateAsync(TEntity entity);

        void UpdateRangeAsync(IEnumerable<TEntity> entity);

        void DeleteAsync(TEntity entity);

        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> inputQuery);

        IQueryable<TEntity> GetQueryableOrderBy(Expression<Func<TEntity, bool>> inputQuery,
            Expression<Func<TEntity, object>> expression, bool isDesc = false);
    }
}
