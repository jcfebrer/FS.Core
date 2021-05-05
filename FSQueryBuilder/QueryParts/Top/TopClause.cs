using FSQueryBuilder.Enums;

//
// Class: TopClause
// Copyright 2006 by Ewout Stortenbeker
// Email: 4ewout@gmail.com
//
// This class is part of the CodeEngine Framework.
// You can download the framework DLL at http://www.code-engine.com/
//

namespace FSQueryBuilder.QueryParts.Top
{
    /// <summary>
    ///     Represents a TOP clause for SELECT statements
    /// </summary>
    public class TopClause
    {
        public int quantity;
        public TopUnit unit;

        public TopClause(int nr)
        {
            quantity = nr;
            unit = TopUnit.Records;
        }

        public TopClause(int nr, TopUnit aUnit)
        {
            quantity = nr;
            unit = aUnit;
        }
    }
}