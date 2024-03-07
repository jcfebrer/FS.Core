namespace FSQueryBuilderCore.QueryParts.Columns
{
    public interface IColumnsConsumer
    {
        ColumnsStatement Columns { get; }
    }
}