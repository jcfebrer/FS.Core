using FSQueryBuilderCore.Constants;

namespace FSQueryBuilderCore.QueryParts.Where
{
    public class OrWhere : OperationWhere
    {
        public OrWhere() : base(WherePatterns.Or)
        {
        }
    }
}