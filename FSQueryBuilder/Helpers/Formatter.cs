using System;
using System.Collections.Generic;
using FSQueryBuilder.Constants;
using FSQueryBuilder.Enums;

namespace FSQueryBuilder.Helpers
{
    internal static class Formatter
    {
        private const string NullString = "NULL";
        private const string CountString = "count(*)";
        private const string Separator = ",";
        private const string Quotes = "'{0}'";
        
        private const string BracesOracle = "\"{0}\"";
        private const string BracesSQL = "[{0}]";
        private const string BracesMySQL = "`{0}`";
        private const string BracesOracleAlias = "\"{0}\" as \"{1}\"";
        private const string BracesSQLAlias = "[{0}] [{1}]";
        private const string BracesMySQLAlias = "`{0}` `{1}`";
        
        private const string Point = "{0}.{1}";
        private const string Spaces = " {0} ";

        public static string FormatSqlValue(object value)
        {
            string formattedValue;

            if (value == null)
            {
                formattedValue = NullString;
            }
            else
            {
                if (value is string)
                {
                    formattedValue = String.Format(Quotes, ((string) value).Replace("'", "''"));
                }
                else if (value is DateTime)
                {
                    formattedValue = String.Format(Quotes, ((DateTime) value).ToString(Patterns.dateTimeCulture));
                }
                else if (value is DBNull)
                {
                    formattedValue = NullString;
                }
                else if (value is byte[])
                {
                    //formattedValue = System.Text.Encoding.ASCII.GetString((byte[])value);
                    //formattedValue = FSLibraryCore.Functions.BytesToString((byte[])value);
                    formattedValue = String.Format(Quotes, (Convert.ToBase64String((byte[])value)));
                }
                else if (value is bool)
                {
                    switch (Dbms.dbmsType)
                    {
                    	case DBMSType.SQLServer:
                    	case DBMSType.Access:
                        case DBMSType.Oracle:
                            formattedValue = (bool) value ? "True" : "False";
                            break;
                        default:
                            formattedValue = (bool) value ? "1" : "0";
                            break;
                    }
                }
                else if (value is SqlLiteral)
                {
                    formattedValue = ((SqlLiteral) value).Value;
                }
                else if (value is Decimal || value is Int32 || value is float || value is Double || value is long || value is int)
                {
                    formattedValue = value.ToString().Replace(",",".");
                }
                else
                {
                    formattedValue = value.ToString();
                }
            }
            return formattedValue;
        }

        public static string FormatColumn(string table, string column, string alias)
        {
            if (column == null)
            {
                return String.Empty;
            }
            string res;
            if (column.ToLower().Contains(CountString))
            {
                res = column;
            }
            else
            {
            	switch(Dbms.dbmsType)
            	{
            		case DBMSType.SQLServer:
            		case DBMSType.Access:
            			if(alias!="")res = String.Format(BracesSQLAlias, column, alias);
            				else res = String.Format(BracesSQL, column);
            			break;
            		case DBMSType.SQLite:
            		case DBMSType.MySQL:
            			if(alias!="")res = String.Format(BracesMySQLAlias, column, alias);
            				else res = String.Format(BracesMySQL, column);
            			break;
            		case DBMSType.Oracle:
            			if(alias!="")res = String.Format(BracesOracleAlias, column, alias);
            				else res = String.Format(BracesOracle, column);
            			break;
            		default:
            			res = String.Format(BracesSQL, column);
            			break;
            	}
                
                if (!String.IsNullOrEmpty(table))
                {
                	res = String.Format(Point, table, res);
                }
            }
            return res;
        }

        public static string FormatStatements(IEnumerable<string> listOfStatements)
        {
            return String.Format(Spaces, String.Join(Separator, ToArray(listOfStatements)));
        }

        private static string[] ToArray(IEnumerable<string> listOfStatements)
        {
            List<string> toArray = new List<string>();
            foreach (string statement in listOfStatements)
            {
                toArray.Add(statement);
            }

            return toArray.ToArray();
        }
    }
}