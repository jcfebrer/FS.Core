using FSQueryBuilder.Constants;

namespace FSQueryBuilder.QueryParts.Where
{
    public class OrWhere : OperationWhere
    {
        public OrWhere() : base(WherePatterns.Or)
        {
        }
    }
}