using System;
using System.Collections.Generic;
using FSQueryBuilder.Exceptions;
using FSQueryBuilder.Helpers;

namespace FSQueryBuilder.QueryParts.Assignments
{
    public class AssignmentStatement
    {
        private readonly List<AssignmentClause> _assignments = new List<AssignmentClause>();

        public void AddAssignment(string column, object value)
        {
            if (String.IsNullOrEmpty(column))
            {
                throw new QueryBuildingException("Column in assignment should not be empty");
            }
            _assignments.Add(new AssignmentClause(Formatter.FormatColumn(String.Empty, column, ""),
                Formatter.FormatSqlValue(value)));
        }

        public string BuildAssignmentStatement()
        {
            if (_assignments.Count == 0)
            {
                throw new QueryBuildingException("List of assignments should not be empty");
            }
            return Formatter.FormatStatements(Select(_assignments));
        }


        private static IEnumerable<string> Select(IEnumerable<AssignmentClause> assignments)
        {
            List<string> assign = new List<string>();
            foreach (AssignmentClause ac in assignments)
            {
                assign.Add(String.Format("{0}={1}", ac.column, ac.value));
            }

            return assign.ToArray();
        }
    }
}