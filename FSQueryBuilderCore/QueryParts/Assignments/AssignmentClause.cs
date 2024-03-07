namespace FSQueryBuilderCore.QueryParts.Assignments
{
    public struct AssignmentClause
    {
        public string column;
        public string value;

        public AssignmentClause(string column, string value)
        {
            this.column = column;
            this.value = value;
        }
    }
}