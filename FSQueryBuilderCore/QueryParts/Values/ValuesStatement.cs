using System.Collections.Generic;
using FSQueryBuilderCore.Exceptions;
using FSQueryBuilderCore.Helpers;

namespace FSQueryBuilderCore.QueryParts.Values
{
    public class ValuesStatement
    {
        private readonly List<object> valuesList = new List<object>();

        public void SelectValues(List<object> values)
        {
            SelectValues(values.ToArray());
        }
        public void SelectValues(params object[] values)
        {
            valuesList.Clear();
            if (values != null)
            {
                foreach (object value in values)
                {
                    valuesList.Add(value);
                }
            }
        }

        public string BuildValuesStatement()
        {
            if (valuesList.Count == 0)
            {
                throw new QueryBuildingException("List of values should not be empty");
            }
            return Formatter.FormatStatements(Select(valuesList));
        }

        private IEnumerable<string> Select(IEnumerable<object> valuesList)
        {
            List<string> values = new List<string>();
            foreach (object value in valuesList)
            {
                values.Add(Formatter.FormatSqlValue(value));
            }

            return values.ToArray();
        }
    }
}