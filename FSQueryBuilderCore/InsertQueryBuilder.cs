using System;
using System.Text;
using FSQueryBuilderCore.Constants;
using FSQueryBuilderCore.Enums;
using FSQueryBuilderCore.QueryParts.Columns;
using FSQueryBuilderCore.QueryParts.Table;
using FSQueryBuilderCore.QueryParts.Values;

namespace FSQueryBuilderCore
{
    public class InsertQueryBuilder : IQueryBuilder, ITableConsumer, IColumnsConsumer, IValuesConsumer
    {
        private const string SpaceAndBraces = " ({0}) ";

        private readonly ColumnsStatement _columns = new ColumnsStatement();

        private readonly ValuesStatement _values = new ValuesStatement();

        private string _returnIdQuery = String.Empty;

        public ColumnsStatement Columns
        {
            get { return _columns; }
        }

        public string BuildQuery()
        {
            StringBuilder res = new StringBuilder();
            res.Append("INSERT INTO ");
            res.Append(TableSource);
            res.Append(String.Format(SpaceAndBraces, Columns.BuildColumnsStatement()));
            res.Append("VALUES");
            res.Append(String.Format(SpaceAndBraces, Values.BuildValuesStatement()));
            if (!String.IsNullOrEmpty(_returnIdQuery))
            {
                res.Append(_returnIdQuery);
            }
            return res.ToString();
        }

        string _TableSource;
        public string TableSource { 
        	get { return _TableSource; }
        	set { _TableSource = value; }
        }

        public ValuesStatement Values
        {
            get { return _values; }
        }

        public void ReturnId(DBMSType dbmsType)
        {
            switch (dbmsType)
            {
                case DBMSType.SQLite:
                    _returnIdQuery = ReturnIdQueries.SqLiteQuery;
                    break;
            }
        }
    }
}