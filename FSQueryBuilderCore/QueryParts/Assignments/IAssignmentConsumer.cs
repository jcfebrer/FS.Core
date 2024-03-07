using FSQueryBuilderCore.QueryParts.Assignments;

namespace FSQueryBuilderCore
{
    public interface IAssignmentConsumer
    {
        AssignmentStatement Assignments { get; }
    }
}