using System;

namespace FSQueryBuilderCore.Exceptions
{
    [Serializable]
    public class QueryBuildingException : Exception
    {
        public QueryBuildingException(string message) : base(message)
        {
        }
    }
}