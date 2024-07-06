using TeachEquipManagement.Utilities.CommonModels;

namespace TeachEquipManagement.DAL.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(object id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task InsertRangeAsync(IEnumerable<TEntity> entity);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> GetQueryable(QueryModel<TEntity> queryCondition);
    }
}
