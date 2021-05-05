using FSQueryBuilder.QueryParts.Assignments;

namespace FSQueryBuilder
{
    public interface IAssignmentConsumer
    {
        AssignmentStatement Assignments { get; }
    }
}