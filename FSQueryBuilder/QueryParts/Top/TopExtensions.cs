using FSQueryBuilder.Enums;

namespace FSQueryBuilder.QueryParts.Top
{
    public static class TopExtensions
    {
        public static void TopRecords(ITopConsumer consumer, int value)
        {
            consumer.TopClause.quantity = value;
            consumer.TopClause.unit = TopUnit.Records;
        }
    }
}