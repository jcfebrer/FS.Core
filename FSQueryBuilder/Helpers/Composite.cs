using System.Collections.Generic;

namespace FSQueryBuilder.Helpers
{
    public abstract class Composite<T> : IComposite<T> where T : class
    {
        protected List<T> collection;

        protected Composite()
        {
            collection = new List<T>();
        }

        public virtual void Add(T element)
        {
            collection.Add(element);
        }

        public virtual void Remove(T element)
        {
            collection.Remove(element);
        }
    }
}