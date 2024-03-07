using System.Collections.Generic;

namespace FSQueryBuilderCore.QueryParts.Join
{
    public interface IJoinConsumer
    {
        List<JoinClause> Joins { get; }
    }
}