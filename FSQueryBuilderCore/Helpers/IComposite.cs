namespace FSQueryBuilderCore
{
    public interface IComposite<T>
    {
        void Add(T element);
        void Remove(T element);
    }
}