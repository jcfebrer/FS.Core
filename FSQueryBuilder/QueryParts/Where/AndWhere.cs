using FSQueryBuilder.Constants;

namespace FSQueryBuilder.QueryParts.Where
{
    public class AndWhere : OperationWhere
    {
        public AndWhere() : base(WherePatterns.And)
        {
        }
    }
}