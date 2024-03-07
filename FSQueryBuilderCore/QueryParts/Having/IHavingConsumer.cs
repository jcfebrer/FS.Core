using FSQueryBuilderCore.QueryParts.Where;

namespace FSQueryBuilderCore.QueryParts.Having
{
    public interface IHavingConsumer
    {
        IWhereExpression Having { get; }
    }
}