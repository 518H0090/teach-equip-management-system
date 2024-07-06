using System.Linq.Expressions;

namespace TeachEquipManagement.DAL.Specifications
{
    public abstract class Specification<TEntity> where TEntity : class
    {
        public Specification(Expression<Func<TEntity, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Expression<Func<TEntity, bool>>? Predicate { get; }

        public List<Expression<Func<TEntity, object>>>? Includes { get; } = 
            new List<Expression<Func<TEntity, object>>>();

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }

        protected void AddInluce(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes?.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderExpression)
        {
            OrderBy = orderExpression;
        }

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderDescExpression)
        {
            OrderByDesc = orderDescExpression;
        }

    }
}
