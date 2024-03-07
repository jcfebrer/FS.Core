using System;
using System.Text;
using FSQueryBuilderCore.Constants;
using FSQueryBuilderCore.Helpers;

namespace FSQueryBuilderCore.QueryParts.Where
{
    public abstract class OperationWhere : Composite<IWhereExpression>, IWhereExpression
    {
        private readonly string operation;

        protected OperationWhere(string operation)
        {
            this.operation = operation;
        }

        public string BuildExpression()
        {
            StringBuilder res = new StringBuilder();
            foreach (IWhereExpression expression in collection)
            {
                res.Append(String.Format(WherePatterns.Braces, expression.BuildExpression()) + operation);
            }
            return RemoveLastOperation(res.ToString());
        }

        private string RemoveLastOperation(string target)
        {
            if (String.IsNullOrEmpty(target)) return target;
            int lastIndex = target.LastIndexOf(operation);
            if (lastIndex != -1 && target.Length - lastIndex == operation.Length)
            {
                target = target.Remove(lastIndex);
            }
            return target;
        }
    }
}