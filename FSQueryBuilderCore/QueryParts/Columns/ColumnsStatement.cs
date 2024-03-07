using System;
using System.Collections.Generic;
using FSQueryBuilderCore.Helpers;

namespace FSQueryBuilderCore.QueryParts.Columns
{
	public class ColumnsStatement
	{
		private readonly List<string> _columnsList = new List<string>();
		private readonly List<string> _columnsListAlias = new List<string>();

        public void SelectColumns(List<string> columns)
        {
            SelectColumns(columns.ToArray());
        }

		public void SelectColumns(params string[] columns)
		{
			_columnsList.Clear();
			if (columns != null) {
				foreach (string column in columns) {
					if (!String.IsNullOrEmpty(column)) {
						_columnsList.Add(column);
					}
				}
			}
		}

        public void AliasColumns(List<string> columns)
        {
            AliasColumns(columns.ToArray());
        }

        public void AliasColumns(params string[] columns)
		{
			_columnsListAlias.Clear();
			if (columns != null) {
				foreach (string column in columns) {
					if (!String.IsNullOrEmpty(column)) {
						_columnsListAlias.Add(column);
					}
				}
			}
		}

		public string BuildColumnsStatement()
		{
			return BuildColumnsStatement(String.Empty);
		}

		public string BuildColumnsStatement(string table)
		{
			if (_columnsList.Count == 0) {
				if (String.IsNullOrEmpty(table))
					return " * ";
				return String.Format(" {0}.* ", table);
			}
			if (_columnsList.Count == 1) {
				if (_columnsList[0] == "*") {
					if (String.IsNullOrEmpty(table))
						return " * ";
					return String.Format(" {0}.* ", table);
				}
			}
			return Formatter.FormatStatements(Select(_columnsList, _columnsListAlias, table));
		}

		private IEnumerable<string> Select(List<string> columnsList, List<string> columnsListAlias, string table)
		{
			List<string> formatColumn = new List<string>();
			int f = 0;
			foreach (string column in columnsList) {
				string alias;
				if (columnsList.Count == columnsListAlias.Count)
					alias = columnsListAlias[f];
				else
					alias = "";
				formatColumn.Add(Formatter.FormatColumn(table, column, alias));
				f++;
			}

			return formatColumn;
		}
	}
}