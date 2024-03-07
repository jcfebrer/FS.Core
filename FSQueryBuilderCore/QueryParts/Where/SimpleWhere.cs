using System;
using System.Collections.Generic;
using FSQueryBuilderCore.Constants;
using FSQueryBuilderCore.Enums;
using FSQueryBuilderCore.Helpers;

namespace FSQueryBuilderCore.QueryParts.Where
{
    public class SimpleWhere : IWhereExpression
    {
        private readonly string columnName;

        private readonly Comparison comparison;
        private readonly string tableName;

        private readonly object value;

        private Dictionary<Comparison, string> comparisonList;

        public SimpleWhere(string tableName, string columnName, Comparison comparison, object value)
        {
            this.tableName = tableName;
            this.columnName = columnName;
            this.value = value;
            this.comparison = comparison;

            FillComparisonList();
        }

        public string BuildExpression()
        {
            return String.Format(comparisonList[comparison],
                Formatter.FormatColumn(tableName, columnName, ""), Formatter.FormatSqlValue(value));
        }

        private void FillComparisonList()
        {
        	comparisonList = new Dictionary<Comparison, string>();
        	
        	comparisonList.Add(Comparison.Equals, WherePatterns.Equal);
        	
            comparisonList.Add(Comparison.NotEquals, WherePatterns.NotEqual);
            comparisonList.Add(Comparison.Like, WherePatterns.Like);
            comparisonList.Add(Comparison.NotLike, WherePatterns.NotLike);
            comparisonList.Add(Comparison.LessThan, WherePatterns.LessThan);
            comparisonList.Add(Comparison.LessOrEquals, WherePatterns.LessThanOrEqual);
            comparisonList.Add(Comparison.GreaterThan, WherePatterns.GreaterThan);
            comparisonList.Add(Comparison.GreaterOrEquals, WherePatterns.GreaterThanOrEqual);
            comparisonList.Add(Comparison.In, WherePatterns.In);
            comparisonList.Add(Comparison.Is, WherePatterns.Is);
        }
    }
}