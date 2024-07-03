using TeachEquipManagement.Utilities.CommonModels;

namespace TeachEquipManagement.DAL.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(object id);

        Task InsertAsync(TEntity entity);

        Task InsertRangeAsync(IEnumerable<TEntity> entity);

        void UpdateAsync(TEntity entity);

        void UpdateRangeAsync(IEnumerable<TEntity> entity);

        void DeleteAsync(TEntity entity);

        IQueryable<TEntity> GetQueryable(QueryModel<TEntity> queryCondition);
    }
}
