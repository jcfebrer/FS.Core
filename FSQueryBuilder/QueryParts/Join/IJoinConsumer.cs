using System.Collections.Generic;

namespace FSQueryBuilder.QueryParts.Join
{
    public interface IJoinConsumer
    {
        List<JoinClause> Joins { get; }
    }
}