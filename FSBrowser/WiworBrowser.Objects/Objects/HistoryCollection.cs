namespace WiworBrowser.Objects
{
	using System;
	using System.Collections;

	/// <summary>
	/// HistoryCollection class
	/// </summary>
	public class HistoryCollection : CollectionBase
	{
		// Constructor
        public HistoryCollection()
		{
		}

		// Get came at the specified index
        public History this[int index]
		{
			get
			{
                return ((History)InnerList[index]);
			}
		}

        // Add new History to the collection
        public void Add(History history)
		{
            InnerList.Add(history);
		}

        // Remove History from the collection
        public void Remove(History history)
		{
            InnerList.Remove(history);
		}

        // Get History with specified name
        public History GetHistory(string url)
		{
            // find the History
            foreach (History history in InnerList)
			{
                if (history.Url == url)
                    return history;
			}
			return null;
		}
	}
}
