using System;
using System.Collections.Generic;
using System.Text;
using FSQueryBuilder.Constants;
using FSQueryBuilder.Enums;
using FSQueryBuilder.Exceptions;

//
// Class: SelectQueryBuilder
// Copyright 2006 by Ewout Stortenbeker
// Email: 4ewout@gmail.com
//
// This class is part of the CodeEngine Framework. This framework also contains
// the UpdateQueryBuilder, InsertQueryBuilder and DeleteQueryBuilder.
// You can download the framework DLL at http://www.code-engine.com/
// 
using FSQueryBuilder.Helpers;
using FSQueryBuilder.QueryParts.Columns;
using FSQueryBuilder.QueryParts.Having;
using FSQueryBuilder.QueryParts.Join;
using FSQueryBuilder.QueryParts.OrderBy;
using FSQueryBuilder.QueryParts.Table;
using FSQueryBuilder.QueryParts.Top;
using FSQueryBuilder.QueryParts.Where;

namespace FSQueryBuilder
{
    public class SelectQueryBuilder : IQueryBuilder, ITableConsumer, IColumnsConsumer, IWhereConsumer, IHavingConsumer,
        IOrderByConsumer, IJoinConsumer, ITopConsumer
    {
        protected ColumnsStatement columns = new ColumnsStatement();
        protected List<string> groupByColumns = new List<string>(); // array of string
        protected List<JoinClause> joins = new List<JoinClause>(); // array of JoinClause
        protected List<OrderByClause> orderByStatement = new List<OrderByClause>(); // array of OrderByClause
        protected TopClause topClause = new TopClause(100, TopUnit.Percent);

        public SelectQueryBuilder()
        {
            Distinct = false;
        }

        bool _Distinct;
        public bool Distinct { 
        	get { return _Distinct;} 
        	set { _Distinct = value;}
        }

        public ColumnsStatement Columns
        {
            get { return columns; }
        }

        IWhereExpression _Having;
        public IWhereExpression Having { 
        	get { return _Having;}
        	set { _Having = value;}
        }

        public List<JoinClause> Joins
        {
            get { return joins; }
        }

        public List<OrderByClause> OrderBy
        {
            get { return orderByStatement; }
        }

        public string BuildQuery()
        {
            StringBuilder res = new StringBuilder();
            res.Append("SELECT ");

            // Output Distinct
            if (Distinct)
            {
                res.Append("DISTINCT ");
            }

            // Output Top clause
            if (!(topClause.quantity == 100 & topClause.unit == TopUnit.Percent))
            {
                res.Append("TOP " + topClause.quantity);
                if (topClause.unit == TopUnit.Percent)
                {
                    res.Append(" PERCENT");
                }
                res.Append(" ");
            }

            // Output column names
            res.Append(Columns.BuildColumnsStatement(TableSource));

            // Output table names
            if (!String.IsNullOrEmpty(TableSource))
            {
                res.Append(" FROM ");
                res.Append(TableSource + ' ');
            }

            // Output joins
            if (joins.Count > 0)
            {
                foreach (JoinClause clause in joins)
                {
                    StringBuilder joinString = new StringBuilder();
                    switch (clause.joinType)
                    {
                        case JoinType.InnerJoin:
                            joinString.Append("INNER JOIN");
                            break;
                        case JoinType.OuterJoin:
                            joinString.Append("OUTER JOIN");
                            break;
                        case JoinType.LeftJoin:
                            joinString.Append("LEFT JOIN");
                            break;
                        case JoinType.RightJoin:
                            joinString.Append("RIGHT JOIN");
                            break;
                    }
                    joinString.Append(" " + clause.toTable + " ON ");
                    joinString.Append(
                        new SimpleWhere(clause.fromTable, clause.fromColumn, clause.comparisonOperator,
                            Formatter.FormatColumn(clause.toTable, clause.toColumn, "")).BuildExpression());
                    res.Append(joinString + " ");
                }
            }

            // Output where statement
            if (Where != null)
            {
                res.Append(" WHERE ");
                res.Append(Where.BuildExpression());
            }

            // Output GroupBy statement
            if (groupByColumns.Count > 0)
            {
                res.Append(" GROUP BY ");
                res.Append(Formatter.FormatStatements(groupByColumns));
            }

            // Output having statement
            if (Having != null)
            {
                // Check if a Group By Clause was set
                if (groupByColumns.Count == 0)
                {
                    throw new QueryBuildingException("Having statement was set without Group By");
                }
                res.Append(" HAVING " + Having.BuildExpression());
            }

            // Output OrderBy statement
            if (orderByStatement.Count > 0)
            {
                res.Append(" ORDER BY ");
                res.Append(
                    Formatter.FormatStatements(
                        Select(orderByStatement)));
            }

            return res.ToString();
        }

        string _TableSource;
        public string TableSource { 
        	get { return _TableSource; }
        	set { _TableSource = value; }
        }

        public TopClause TopClause
        {
            get { return topClause; }
        }

        IWhereExpression _Where;
        public IWhereExpression Where {
        	get { return _Where;}
        	set { _Where = value;}
        }

        public void GroupBy(params string[] cols)
        {
            foreach (string column in cols)
            {
                groupByColumns.Add(column);
            }
        }

        private IEnumerable<string> Select(IEnumerable<OrderByClause> orderBy)
        {
            List<string> values = new List<string>();
            foreach (OrderByClause clause in orderBy)
            {
                values.Add((clause.fieldName + (clause.sortOrder == Sorting.Ascending ? " ASC" : " DESC")));
            }

            return values.ToArray();
        }
    }
}