using System.Linq.Expressions;

namespace TeachEquipManagement.Utilities.CommonModels
{
    public class QueryModel<TEntity> where TEntity : class 
    {
        public Expression<Func<TEntity, bool>>? QueryCondition { set; get; }

        public Expression<Func<TEntity, object>>? OrderCondition { set; get; }

        public bool? IsAsNoTracking { get; set; }

        public bool? IsOrderDescending { get; set; }
    }
}
