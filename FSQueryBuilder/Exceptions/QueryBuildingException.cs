using System;

namespace FSQueryBuilder.Exceptions
{
    [Serializable]
    public class QueryBuildingException : Exception
    {
        public QueryBuildingException(string message) : base(message)
        {
        }
    }
}