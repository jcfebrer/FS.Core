namespace FSQueryBuilder.QueryParts.Values
{
    public static class ValuesExtensions
    {
        public static void SelectValues(IValuesConsumer consumer, params object[] values)
        {
            consumer.Values.SelectValues(values);
        }
    }
}