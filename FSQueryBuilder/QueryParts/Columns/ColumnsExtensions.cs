namespace FSQueryBuilder.QueryParts.Columns
{
    public static class ColumnsExtensions
    {
        public static void SelectAllColumns(IColumnsConsumer consumer)
        {
            consumer.Columns.SelectColumns();
        }

        public static void SelectCount(IColumnsConsumer consumer)
        {
            consumer.Columns.SelectColumns("count(*)");
        }

        public static void SelectColumns(IColumnsConsumer consumer, params string[] columns)
        {
            consumer.Columns.SelectColumns(columns);
        }
    }
}