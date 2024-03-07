namespace FSQueryBuilderCore.QueryParts.Table
{
    public static class TableExtensions
    {
        public static void SetTable(ITableConsumer consumer, string table)
        {
            consumer.TableSource = table;
        }
    }
}