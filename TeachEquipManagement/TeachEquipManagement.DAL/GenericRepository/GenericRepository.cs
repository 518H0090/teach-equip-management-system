using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.Specifications;

namespace TeachEquipManagement.DAL.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
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

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> inputQuery,
            bool isAsNoTracking = false)
        {
            if (isAsNoTracking)
            {
                return _context.Set<TEntity>().Where(inputQuery).AsNoTracking();
            }

            return _context.Set<TEntity>().Where(inputQuery);
        }

        public IQueryable<TEntity> GetQueryableOrderBy(Expression<Func<TEntity, bool>> inputQuery, 
            Expression<Func<TEntity, object>> expression, bool isDesc = false)
        {
            if (!isDesc)
            {
                return _context.Set<TEntity>().Where(inputQuery).OrderBy(expression);
            }

            return _context.Set<TEntity>().Where(inputQuery).OrderByDescending(expression);
        }

    }
}
