using FSQueryBuilderCore.Constants;

namespace FSQueryBuilderCore.QueryParts.Where
{
    public class AndWhere : OperationWhere
    {
        public AndWhere() : base(WherePatterns.And)
        {
        }
    }
}