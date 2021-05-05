namespace WiworBrowser.Objects
{
	using System;
	
	/// <summary>
	/// History class
	/// </summary>
	public class History
	{
		private string	url;
		private DateTime lastVisited;
		private int	times;
		
		// Name property
		public string Url
		{
			get { return url; }
			set { url = value; }
		}
		// lastVisited property
		public DateTime LastVisited
		{
			get { return lastVisited; }
			set { lastVisited = value; }
		}
        // times property
        public int Times
        {
            get { return times; }
            set { times = value; }
        }
        

		// Constructor
		public History(string url)
		{
			this.url = url;
		}

        public History(string url, DateTime lastVisited, int times)
        {
            this.url = url;
            this.lastVisited = lastVisited;
            this.times = times;
        }
	}
}
