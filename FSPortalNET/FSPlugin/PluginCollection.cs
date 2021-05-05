using System;
using System.Collections;
using FSLibrary;
using FSException;

namespace FSPlugin
{
    /// <summary>
    ///     Represents a collection of <see cref="IPlugin">IPlugin</see> objects.
    /// </summary>
    public class PluginCollection : CollectionBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PluginCollection">PluginCollection</see> class.
        /// </summary>
        public PluginCollection()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PluginCollection">PluginCollection</see> class containing the elements
        ///     of the specified source collection.
        /// </summary>
        /// <param name="value">A <see cref="PluginCollection">PluginCollection</see> with which to initialize the collection.</param>
        public PluginCollection(PluginCollection value)
        {
            AddRange(value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PluginCollection">PluginCollection</see> class containing the
        ///     specified array of <see cref="IPlugin">IPlugin</see> objects.
        /// </summary>
        /// <param name="value">An array of <see cref="IPlugin">IPlugin</see> objects with which to initialize the collection. </param>
        public PluginCollection(IPlugin[] value)
        {
            AddRange(value);
        }

        /// <summary>
        ///     Gets the <see cref="PluginCollection">PluginCollection</see> at the specified index in the collection.
        ///     <para>
        ///         In C#, this property is the indexer for the <see cref="PluginCollection">PluginCollection</see> class.
        ///     </para>
        /// </summary>
        public IPlugin this[int index]
        {
            get { return ((IPlugin) (List[index])); }
        }


        public int Add(IPlugin value)
        {
            return List.Add(value);
        }

        /// <summary>
        ///     Copies the elements of the specified <see cref="IPlugin">IPlugin</see> array to the end of the collection.
        /// </summary>
        /// <param name="value">An array of type <see cref="IPlugin">IPlugin</see> containing the objects to add to the collection.</param>
        public void AddRange(IPlugin[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        ///     Adds the contents of another <see cref="PluginCollection">PluginCollection</see> to the end of the collection.
        /// </summary>
        /// <param name="value">
        ///     A <see cref="PluginCollection">PluginCollection</see> containing the objects to add to the
        ///     collection.
        /// </param>
        public void AddRange(PluginCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add((IPlugin) value.List[i]);
            }
        }

        public IPlugin GetPlugin(string name)
        {
            foreach (IPlugin ip in List)
            {
                if (ip.Name.ToLower() == name.ToLower()) return ip;
            }
            return null;
        }

        public string Execute(string plugin)
        {
            return Execute(plugin, new string[1]);
        }

        public string Execute(string plugin, params string[] p)
        {
            IPlugin iPlugin = GetPlugin(plugin);

            if (iPlugin == null)
            {
                throw new ExceptionUtil("Plugin inexistente: " + plugin);
            }

            if (iPlugin.Parameters + 1 != p.Length)
            {
                throw new ExceptionUtil("Parámetros incorrectos (" + iPlugin.Parameters + ") en el Plugin: " + plugin);
            }
            return iPlugin.Execute(p);
        }

        public string Execute(int plugin, params string[] p)
        {
            IPlugin iPlugin = this[plugin];

            if (iPlugin.Parameters + 1 != p.Length)
            {
                throw new ExceptionUtil("Parámetros incorrectos (" + iPlugin.Parameters + ") en el Plugin: " + plugin);
            }
            return iPlugin.Execute(p);
        }

        /// <summary>
        ///     Gets a value indicating whether the collection contains the specified
        ///     <see cref="PluginCollection">PluginCollection</see>.
        /// </summary>
        /// <param name="value">The <see cref="PluginCollection">PluginCollection</see> to search for in the collection.</param>
        /// <returns><b>true</b> if the collection contains the specified object; otherwise, <b>false</b>.</returns>
        public bool Contains(IPlugin value)
        {
            return List.Contains(value);
        }

        /// <summary>
        ///     Copies the collection objects to a one-dimensional <see cref="T:System.Array">Array</see> instance beginning at the
        ///     specified index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array">Array</see> that is the destination of the values
        ///     copied from the collection.
        /// </param>
        /// <param name="index">The index of the array at which to begin inserting.</param>
        public void CopyTo(IPlugin[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///     Creates a one-dimensional <see cref="T:System.Array">Array</see> instance containing the collection items.
        /// </summary>
        /// <returns>Array of type IPlugin</returns>
        public IPlugin[] ToArray()
        {
            IPlugin[] array = new IPlugin[Count];
            CopyTo(array, 0);

            return array;
        }

        /// <summary>
        ///     Gets the index in the collection of the specified <see cref="PluginCollection">PluginCollection</see>, if it exists
        ///     in the collection.
        /// </summary>
        /// <param name="value">The <see cref="PluginCollection">PluginCollection</see> to locate in the collection.</param>
        /// <returns>The index in the collection of the specified object, if found; otherwise, -1.</returns>
        public int IndexOf(IPlugin value)
        {
            return List.IndexOf(value);
        }

        public void Insert(int index, IPlugin value)
        {
            List.Insert(index, value);
        }

        public void Remove(IPlugin value)
        {
            List.Remove(value);
        }

        /// <summary>
        ///     Returns an enumerator that can iterate through the <see cref="PluginCollection">PluginCollection</see> instance.
        /// </summary>
        /// <returns>
        ///     An <see cref="PluginCollectionEnumerator">PluginCollectionEnumerator</see> for the
        ///     <see cref="PluginCollection">PluginCollection</see> instance.
        /// </returns>
        //public new PluginCollectionEnumerator GetEnumerator()
        //{
        //    return new PluginCollectionEnumerator(this);  
        //}

        /// <summary>
        ///     Supports a simple iteration over a <see cref="PluginCollection">PluginCollection</see>.
        /// </summary>
        public class PluginCollectionEnumerator : IEnumerator
        {
            private readonly IEnumerator enumerator;

            /// <summary>
            ///     Initializes a new instance of the <see cref="PluginCollectionEnumerator">PluginCollectionEnumerator</see> class
            ///     referencing the specified <see cref="PluginCollection">PluginCollection</see> object.
            /// </summary>
            /// <param name="mappings">The <see cref="PluginCollection">PluginCollection</see> to enumerate.</param>
            public PluginCollectionEnumerator(PluginCollection mappings)
            {
                enumerator = mappings.GetEnumerator();
            }

            /// <summary>
            ///     Gets the current element in the collection.
            /// </summary>
            public IPlugin Current
            {
                get { return ((IPlugin) (enumerator.Current)); }
            }

            object IEnumerator.Current
            {
                get { return enumerator.Current; }
            }

            bool IEnumerator.MoveNext()
            {
                return enumerator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                enumerator.Reset();
            }

            /// <summary>
            ///     Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            ///     <b>true</b> if the enumerator was successfully advanced to the next element; <b>false</b> if the enumerator
            ///     has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            /// <summary>
            ///     Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                enumerator.Reset();
            }
        }
    }
}