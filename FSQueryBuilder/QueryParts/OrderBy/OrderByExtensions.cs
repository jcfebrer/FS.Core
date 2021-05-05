using System;
using FSQueryBuilder.Enums;

namespace FSQueryBuilder.QueryParts.OrderBy
{
    public static class OrderByExtensions
    {
        public static void AddOrderBy(IOrderByConsumer consumer, OrderByClause clause)
        {
            consumer.OrderBy.Add(clause);
        }

        public static void AddOrderBy(IOrderByConsumer consumer, Enum field, Sorting order)
        {
            OrderByClause newOrderByClause = new OrderByClause(field.ToString(), order);
            consumer.OrderBy.Add(newOrderByClause);
        }

        public static void AddOrderBy(IOrderByConsumer consumer, string field, Sorting order)
        {
            OrderByClause newOrderByClause = new OrderByClause(field, order);
            consumer.OrderBy.Add(newOrderByClause);
        }
    }
}