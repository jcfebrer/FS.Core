using FSQueryBuilderCore.Enums;

//
// Class: OrderByClause
// Copyright 2006 by Ewout Stortenbeker
// Email: 4ewout@gmail.com
//
// This class is part of the CodeEngine Framework.
// You can download the framework DLL at http://www.code-engine.com/
//

namespace FSQueryBuilderCore.QueryParts.OrderBy
{
    /// <summary>
    ///     Represents a ORDER BY clause to be used with SELECT statements
    /// </summary>
    public struct OrderByClause
    {
        public string fieldName;
        public Sorting sortOrder;

        public OrderByClause(string field)
        {
            fieldName = field;
            sortOrder = Sorting.Ascending;
        }

        public OrderByClause(string field, Sorting order)
        {
            fieldName = field;
            sortOrder = order;
        }
    }
}