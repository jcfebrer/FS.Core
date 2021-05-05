namespace FSQueryBuilder.Constants
{
    public static class WherePatterns
    {
        public const string Equal = "{0} = {1}";
        public const string NotEqual = "{0} <> {1}";
        public const string GreaterThan = "{0} > {1}";
        public const string LessThan = "{0} < {1}";
        public const string GreaterThanOrEqual = "{0} >= {1}";
        public const string LessThanOrEqual = "{0} <= {1}";
        public const string Like = "{0} LIKE {1}";
        public const string Is = "{0} IS {1}";
        public const string NotLike = "NOT {0} LIKE {1}";
        public const string In = "{0} IN ({1})";
        public const string Braces = "({0})";
        public const string And = " AND ";
        public const string Or = " OR ";
    }
}