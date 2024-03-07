using System.Collections.Generic;

namespace FSQueryBuilderCore.QueryParts.OrderBy
{
    public interface IOrderByConsumer
    {
        List<OrderByClause> OrderBy { get; }
    }
}