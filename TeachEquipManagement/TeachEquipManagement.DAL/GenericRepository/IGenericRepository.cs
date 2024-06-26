namespace TeachEquipManagement.DAL.GenericRepository
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetBydIdAsync(Guid id);

        Task InsertAsync(TEntity entity);

        Task InsertRangeAsync(IEnumerable<TEntity> entity);

        void UpdateAsync(TEntity entity);

        void UpdateRangeAsync(IEnumerable<TEntity> entity);

        void DeleteAsync(TEntity entity);
    }
}
