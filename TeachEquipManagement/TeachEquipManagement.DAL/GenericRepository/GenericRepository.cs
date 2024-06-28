using Microsoft.EntityFrameworkCore;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.Utilities.CommonModels;

namespace TeachEquipManagement.DAL.GenericRepository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public void DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetBydIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entity)
        {
            await _context.Set<TEntity>().AddRangeAsync(entity);
        }

        public void UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void UpdateRangeAsync(IEnumerable<TEntity> entity)
        {
            _context.Set<TEntity>().UpdateRange(entity);
        }

        public IQueryable<TEntity> GetQueryable(QueryModel<TEntity> queryCondition)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (queryCondition.QueryCondition != null)
            {
                query = query.Where(queryCondition.QueryCondition);

                if (queryCondition.IsAsNoTracking.HasValue)
                {
                    query = query.AsNoTracking();
                }
            }

            if (queryCondition.OrderCondition != null)
            {
                if (queryCondition.IsOrderDescending.HasValue)
                {
                    query = query.OrderByDescending(queryCondition.OrderCondition);
                }

                else
                {
                    query = query.OrderBy(queryCondition.OrderCondition);
                }
            }

            return query;
        }

    }
}
