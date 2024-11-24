namespace FSFormControlsCore
{
    public class DBGridViewFilter
    {
        public enum FilterComparisionOperator
        {
            StartsWith,
            EndWith,
            Contains
        }

        public string CompareValue { get; set; }
        public FilterComparisionOperator ComparisionOperator { get; set; }
        public string Name { get; internal set; }
    }
}