namespace FSQueryBuilder.QueryParts.Assignments
{
    public static class AssignmentExtensions
    {
        public static void AddAssignment(IAssignmentConsumer consumer, string column, object value)
        {
            consumer.Assignments.AddAssignment(column, value);
        }
    }
}