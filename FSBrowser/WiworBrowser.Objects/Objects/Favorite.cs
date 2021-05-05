namespace WiworBrowser.Objects
{
	using System;
	
	/// <summary>
	/// Favorite class
	/// </summary>
	public class Favorite
	{
		private string	url;
		private string	description = "";
		private Group	parent = null;
		
		// url property
		public string Url
		{
			get { return url; }
			set { url = value; }
		}
		// Description property
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
        
		// Parent property
		public Group Parent
		{
			get { return parent; }
			set { parent = value; }
		}
		// FullName property
		public string FullName
		{
			get
			{
				return (parent == null) ? url : (parent.FullName + '\\' + url);
			}
		}


		// Constructor
		public Favorite(string url)
		{
			this.url = url;
		}

        public Favorite(string url, string desc)
        {
            this.url = url;
            this.description = desc;
        }

        public Favorite(string url, string desc, Group parent)
        {
            this.url = url;
            this.description = desc;
            this.parent = parent;
        }
	}
}
