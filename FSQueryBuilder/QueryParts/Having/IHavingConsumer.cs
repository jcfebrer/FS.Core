using FSQueryBuilder.QueryParts.Where;

namespace FSQueryBuilder.QueryParts.Having
{
    public interface IHavingConsumer
    {
        IWhereExpression Having { get; }
    }
}