using Microsoft.EntityFrameworkCore;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.Utilities.CommonModels;

namespace TeachEquipManagement.DAL.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var Entity = await _context.Set<TEntity>().AddAsync(entity);
            return Entity.Entity;
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entity)
        {
            await _context.Set<TEntity>().AddRangeAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entity)
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
