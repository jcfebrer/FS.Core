using FSQueryBuilder.Enums;

namespace FSQueryBuilder.QueryParts.Join
{
    public static class JoinExtensions
    {
        public static void AddJoin(IJoinConsumer consumer, JoinClause newJoin)
        {
            consumer.Joins.Add(newJoin);
        }

        public static void AddJoin(IJoinConsumer consumer, JoinType join, string toTableName,
            string toColumnName, Comparison @operator, string fromTableName, string fromColumnName)
        {
            JoinClause newJoin = new JoinClause(join, toTableName, toColumnName, @operator, fromTableName,
                fromColumnName);
            consumer.Joins.Add(newJoin);
        }
    }
}