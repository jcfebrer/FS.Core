//
// Class: SqlLiteral
// Copyright 2006 by Ewout Stortenbeker
// Email: 4ewout@gmail.com
//
// This class is part of the CodeEngine Framework.
// You can download the framework DLL at http://www.code-engine.com/
// 

namespace FSQueryBuilder
{
    public class SqlLiteral
    {
        public SqlLiteral(string value)
        {
            Value = value;
        }

        string _Value;
        public string Value { 
        	get { return _Value;}
        	set { _Value = value;}
        }
    }
}