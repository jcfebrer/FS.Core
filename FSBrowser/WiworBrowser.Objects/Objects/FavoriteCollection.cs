namespace WiworBrowser.Objects
{
	using System;
	using System.Collections;

	/// <summary>
	/// FavoriteCollection class
	/// </summary>
	public class FavoriteCollection : CollectionBase
	{
		// Constructor
		public FavoriteCollection()
		{
		}

		// Get came at the specified index
		public Favorite this[int index]
		{
			get
			{
				return ((Favorite) InnerList[index]);
			}
		}

		// Add new Favorite to the collection
		public void Add(Favorite favorite)
		{
			InnerList.Add(favorite);
		}

		// Remove Favorite from the collection
		public void Remove(Favorite favorite)
		{
			InnerList.Remove(favorite);
		}

		// Get Favorite with specified name and parent
		public Favorite GetFavorite(string url, Group parent)
		{
			// find the Favorite
			foreach (Favorite favorite in InnerList)
			{
				if ((favorite.Url == url) && (favorite.Parent == parent))
					return favorite;
			}
			return null;
		}

        // Get Favorite with specified name
        public Favorite GetFavorite(string url)
        {
            // find the Favorite
            foreach (Favorite favorite in InnerList)
            {
                if (favorite.Url == url)
                    return favorite;
            }
            return null;
        }
	}
}
