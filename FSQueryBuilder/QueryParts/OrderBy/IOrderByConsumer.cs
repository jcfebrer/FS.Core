using System.Collections.Generic;

namespace FSQueryBuilder.QueryParts.OrderBy
{
    public interface IOrderByConsumer
    {
        List<OrderByClause> OrderBy { get; }
    }
}