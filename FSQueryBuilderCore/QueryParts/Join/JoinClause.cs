using FSQueryBuilderCore.Enums;

//
// Class: JoinClause
// Copyright 2006 by Ewout Stortenbeker
// Email: 4ewout@gmail.com
//
// This class is part of the CodeEngine Framework.
// You can download the framework DLL at http://www.code-engine.com/
//

namespace FSQueryBuilderCore.QueryParts.Join
{
    /// <summary>
    ///     Represents a JOIN clause to be used with SELECT statements
    /// </summary>
    public class JoinClause
    {
        public Comparison comparisonOperator;
        public string fromColumn;
        public string fromTable;
        public JoinType joinType;
        public string toColumn;
        public string toTable;

        public JoinClause(JoinType join, string toTableName, string toColumnName, Comparison @operator,
            string fromTableName, string fromColumnName)
        {
            joinType = join;
            fromTable = fromTableName;
            fromColumn = fromColumnName;
            comparisonOperator = @operator;
            toTable = toTableName;
            toColumn = toColumnName;
        }
    }
}