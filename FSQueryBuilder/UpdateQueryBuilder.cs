using System.Text;
using FSQueryBuilder.QueryParts.Assignments;
using FSQueryBuilder.QueryParts.Table;
using FSQueryBuilder.QueryParts.Where;

namespace FSQueryBuilder
{
    public class UpdateQueryBuilder : IQueryBuilder, ITableConsumer, IAssignmentConsumer, IWhereConsumer
    {
        private readonly AssignmentStatement assignments = new AssignmentStatement();

        public AssignmentStatement Assignments
        {
            get { return assignments; }
        }

        public string BuildQuery()
        {
            StringBuilder res = new StringBuilder();
            res.Append("UPDATE ");
            res.Append(TableSource);
            res.Append(" SET ");
            res.Append(Assignments.BuildAssignmentStatement());
            if (Where != null)
            {
                res.Append(" WHERE ");
                res.Append(Where.BuildExpression());
            }
            return res.ToString();
        }

        string _TableSource;
        public string TableSource {
        	get { return _TableSource; }
        	set { _TableSource = value; }
        }
        
        IWhereExpression _Where;
        public IWhereExpression Where { 
        	get { return _Where; }
        	set {_Where = value; }
        }
    }
}