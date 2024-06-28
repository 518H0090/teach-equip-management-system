using System.Data.Entity;

namespace TeachEquipManagement.DAL.Specifications
{
    public static class SpecificationQueryBuilder
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(
                IQueryable<TEntity> inputQuery,
                Specification<TEntity> specification
            ) 
            where TEntity : class
        {
            IQueryable<TEntity> queryable = inputQuery;

            if (specification.Predicate != null) { 
                queryable = queryable.Where(specification.Predicate);
            }

            if (specification.Includes != null) {
                queryable = specification.Includes.Aggregate(queryable, (current, include) =>
                {
                    return current.Include(include);
                });
            }

            if (specification.OrderBy != null) { 
                queryable.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDesc != null)
            {
                queryable.OrderByDescending(specification.OrderByDesc);
            }

            return queryable;
        }

    }
}
