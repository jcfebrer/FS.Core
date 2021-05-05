using System.Text;
using FSQueryBuilder.QueryParts.Table;
using FSQueryBuilder.QueryParts.Where;

namespace FSQueryBuilder
{
    public class DeleteQueryBuilder : IQueryBuilder, ITableConsumer, IWhereConsumer
    {
        public string BuildQuery()
        {
            StringBuilder res = new StringBuilder();
            res.Append("DELETE FROM ");
            res.Append(TableSource + " ");
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
        	set { _Where = value; }
        }
    }
}