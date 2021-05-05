namespace FSQueryBuilder.QueryParts.Where
{
    public interface IWhereConsumer
    {
        IWhereExpression Where { get; set; }
    }
}