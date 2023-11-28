using FSQueryBuilder.Enums;

namespace FSQueryBuilder.QueryParts.Top
{
    public static class TopExtensions
    {
        public static void TopRecords(ITopConsumer consumer, int value)
        {
            consumer.Top.quantity = value;
            consumer.Top.unit = TopUnit.Records;
        }
    }
}