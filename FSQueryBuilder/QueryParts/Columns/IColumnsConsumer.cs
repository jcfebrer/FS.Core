namespace FSQueryBuilder.QueryParts.Columns
{
    public interface IColumnsConsumer
    {
        ColumnsStatement Columns { get; }
    }
}